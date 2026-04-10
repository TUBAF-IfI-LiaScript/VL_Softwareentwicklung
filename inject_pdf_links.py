#!/usr/bin/env python3
"""
inject_pdf_links.py

Reads a LiaScript-exporter-generated HTML file, finds course cards in the
JSON-LD metadata, constructs the expected GitHub Releases download URL for
each course's PDF, checks whether the release asset exists, and injects a
download link into every matching card.

Usage:
    python inject_pdf_links.py <input.html> <output.html>

The script is intentionally self-contained (stdlib + beautifulsoup4 +
requests) so it can run directly in a GitHub Actions workflow.
"""

import hashlib
import json
import os
import re
import sys
from pathlib import Path
from urllib.parse import urlparse
import posixpath

import requests
from bs4 import BeautifulSoup
from bs4.element import Tag


# ---------------------------------------------------------------------------
# HTML helpers
# ---------------------------------------------------------------------------

def load_html(filepath: str) -> BeautifulSoup:
    with open(filepath, encoding="utf-8") as fh:
        return BeautifulSoup(fh, "html.parser")


def save_html(soup: BeautifulSoup, filepath: str) -> None:
    with open(filepath, "w", encoding="utf-8") as fh:
        fh.write(str(soup))


# ---------------------------------------------------------------------------
# JSON-LD parsing
# ---------------------------------------------------------------------------

def extract_json_ld(soup: BeautifulSoup) -> dict:
    script_tag = soup.find("script", type="application/ld+json")
    if not script_tag:
        raise ValueError("No JSON-LD <script> tag found in the HTML.")
    return json.loads(script_tag.string)


def _hash_id(id_url: str) -> str:
    return hashlib.sha256(id_url.encode("utf-8")).hexdigest()


def _file_basename(id_url: str) -> str:
    """Return the basename (with extension) of the URL path."""
    return posixpath.basename(urlparse(id_url).path)


# ---------------------------------------------------------------------------
# Safe-tag / release-URL generation
#
# This must stay in sync with the safe_tag logic in generate_pdfs.yml.
# ---------------------------------------------------------------------------

_UMLAUT_MAP = str.maketrans(
    {
        "ä": "ae",
        "Ä": "ae",
        "ö": "oe",
        "Ö": "oe",
        "ü": "ue",
        "Ü": "ue",
        "ß": "ss",
    }
)


def _safe_tag(name: str) -> str:
    """Convert a filename (without extension) to a safe release tag component."""
    result = name.lower().translate(_UMLAUT_MAP)
    result = re.sub(r"[^a-z0-9_-]", "", result)
    return result


def _releases_base_url(id_url: str) -> str:
    """
    Derive the GitHub releases/download base URL from a raw GitHub URL.

    Examples
    --------
    https://raw.githubusercontent.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/master/00_Einfuehrung.md
    -> https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/releases/download
    """
    parsed = urlparse(id_url)
    host = parsed.netloc.lower()
    parts = [p for p in parsed.path.split("/") if p]

    if host not in ("raw.githubusercontent.com", "github.com"):
        raise ValueError(f"Not a recognised GitHub URL: {id_url!r}")
    if len(parts) < 2:
        raise ValueError(f"Cannot extract owner/repo from URL: {id_url!r}")

    owner, repo = parts[0], parts[1]
    return f"https://github.com/{owner}/{repo}/releases/download"


def build_pdf_url(id_url: str, version: str) -> str:
    """
    Build the expected GitHub Releases download URL for the PDF asset.

    Tag format  : {safe_name}_v{version}
    Asset format: {original_name}_v{version}_Documentation.pdf
    """
    base_name = os.path.splitext(_file_basename(id_url))[0]
    tag = f"{_safe_tag(base_name)}_v{version}"
    asset = f"{base_name}_v{version}_Documentation.pdf"
    base_url = _releases_base_url(id_url)
    return f"{base_url}/{tag}/{asset}"


# ---------------------------------------------------------------------------
# Release-existence check
# ---------------------------------------------------------------------------

def release_exists(url: str, timeout: int = 10) -> bool:
    """Return True when the asset URL responds with 200 or 302."""
    try:
        resp = requests.head(url, timeout=timeout, allow_redirects=True)
        print(f"  HEAD {url} -> {resp.status_code}")
        return resp.status_code in (200, 302)
    except Exception as exc:
        print(f"  HEAD {url} -> error: {exc}")
        return False


# ---------------------------------------------------------------------------
# Build the course-info lookup table from JSON-LD
# ---------------------------------------------------------------------------

def build_course_map(json_ld: dict) -> dict:
    """
    Returns a dict keyed by SHA-256( id_url ) with values::

        {
            "id_url":   str,
            "version":  str,
            "pdf_url":  str,
        }
    """
    result: dict = {}
    outer_list = json_ld.get("itemListElement", [])
    for outer in outer_list:
        for item in outer.get("itemListElement", []):
            id_url = item.get("@id", "")
            version = item.get("version", "")
            if not id_url or not version:
                continue
            pdf_url = build_pdf_url(id_url, version)
            entry = {"id_url": id_url, "version": version, "pdf_url": pdf_url}
            result[_hash_id(id_url)] = entry
    return result


# ---------------------------------------------------------------------------
# Card selection and link injection
# ---------------------------------------------------------------------------

def _get_cards(soup: BeautifulSoup):
    """Return the inner card-body elements that represent individual courses."""
    return soup.select("div.card-body div.row div.card-body")


def _get_card_course_key(card, course_map: dict) -> str | None:
    """Return the course_map key for the course card, or None if not found."""
    link = card.find("a", href=True)
    if not link:
        return None
    href = link["href"]
    id_url = href.split("?", 1)[1] if "?" in href else None
    if not id_url:
        return None
    key = _hash_id(id_url)
    return key if key in course_map else None


def _build_pdf_button(soup: BeautifulSoup, pdf_url: str) -> Tag:
    """Create a small download-link element for the PDF."""
    wrapper = soup.new_tag("div", **{"class": "card-links"})
    ul = soup.new_tag("ul", **{"class": "list-inline"})
    li = soup.new_tag("li", **{"class": "list-inline-item"})
    a = soup.new_tag("a", href=pdf_url, target="_blank", rel="noopener noreferrer")
    a.string = "📄 PDF"
    li.append(a)
    ul.append(li)
    wrapper.append(ul)
    return wrapper


def _build_no_release_span(soup: BeautifulSoup) -> Tag:
    span = soup.new_tag("span", **{"class": "release-not-found"})
    span.string = "PDF not yet available."
    return span


def inject_pdf_links(soup: BeautifulSoup, course_map: dict) -> int:
    """
    Walk every course card, check for an existing PDF release and inject a
    download link.  Returns the number of cards that received a link.
    """
    injected = 0
    for card in _get_cards(soup):
        key = _get_card_course_key(card, course_map)
        if key is None:
            continue
        info = course_map[key]
        pdf_url = info["pdf_url"]
        print(f"Processing: {info['id_url']} (v{info['version']})")

        if release_exists(pdf_url):
            element = _build_pdf_button(soup, pdf_url)
            injected += 1
        else:
            element = _build_no_release_span(soup)

        # Insert after the card
        parent = card.parent
        parent.insert(list(parent.contents).index(card) + 1, element)

    return injected


# ---------------------------------------------------------------------------
# CSS link injection
# ---------------------------------------------------------------------------

def add_css_link(soup: BeautifulSoup, href: str = "linklayout.css") -> None:
    head = soup.find("head")
    if not head:
        return
    link_tag = soup.new_tag("link", rel="stylesheet", href=href)
    head.append(link_tag)


# ---------------------------------------------------------------------------
# Entry point
# ---------------------------------------------------------------------------

def main(input_html: str, output_html: str) -> None:
    print(f"Loading: {input_html}")
    soup = load_html(input_html)

    print("Extracting JSON-LD ...")
    json_ld = extract_json_ld(soup)

    print("Building course map ...")
    course_map = build_course_map(json_ld)
    print(f"  Found {len(course_map)} courses in JSON-LD.")

    print("Injecting PDF links ...")
    count = inject_pdf_links(soup, course_map)
    print(f"  Injected links for {count} courses.")

    add_css_link(soup)

    print(f"Saving: {output_html}")
    save_html(soup, output_html)
    print("Done.")


if __name__ == "__main__":
    if len(sys.argv) != 3:
        print("Usage: inject_pdf_links.py <input.html> <output.html>", file=sys.stderr)
        sys.exit(1)
    main(sys.argv[1], sys.argv[2])
