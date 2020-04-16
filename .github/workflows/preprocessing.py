import glob, os
import shutil
import re

blackList = ["{{", "******", "@Rextester"]

for file in glob.glob("*.md"):
    if file != "README.md":
        shutil.copy2(file, file+"x")

for file in glob.glob("*.mdx"):
    content = open(file, 'r').readlines()
    filtered = []
    for line in content:
        if any(entry in line for entry in blackList):
            continue
        result = re.search( r'```\S+', line)
        if result:
            line = result.group(0) + "\n"
        filtered.append(line)

    with open(file, "w") as outfile:
        outfile.write("".join(filtered))

# pandoc  --toc --top-level-division=chapter -V geometry:margin=2cm --listings -H listings.tex -V linkcolor:blue --pdf-engine=xelatex -o doc.pdf *.mdx
