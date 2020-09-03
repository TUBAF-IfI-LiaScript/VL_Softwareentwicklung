import glob, os
import shutil
import re

blackList = ["{{", "******", "@Rextester", "@Tau"]

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
        if file.endswith("00_Einfuehrung.mdx"):  # insert metadata for pandoc in first md file
            title = "**C# Kurs TU Freiberg**"
            outfile.write(f"---\ntitle: |\n  {title}\n  https://github.com/SebastianZug/CsharpCourse\nauthor:\n")
            with open(".github/workflows/authors.txt", "r") as authors:  # read in authors and write them into the yaml code
                for line in authors:
                    name = line.strip()
                    outfile.write(f"  - {name}\n")

            outfile.write("papersize:\n  - a4\ngeometry:\n  - margin=2cm\n")
            outfile.write("header-includes:\n  - \\usepackage{titling}\n  - \\pretitle{\\begin{center}\n    \\includegraphics[width=2cm]{img/Logo_TU_Freiberg.png}\\LARGE\\\\}\n  - \\posttitle{\\end{center}}\n---\n")
        outfile.write("".join(filtered))

# pandoc  --toc --top-level-division=chapter -V geometry:margin=2cm --listings -H listings.tex -V linkcolor:blue --pdf-engine=xelatex -o doc.pdf *.mdx
