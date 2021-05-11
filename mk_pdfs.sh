#!/bin/bash
if [ ! -d output ]; then
mkdir output
fi

python scripts/preprocessing.py

for file in output/*.mdx; do
	echo "$file"
	pandoc -V linkcolor:blue .github/workflows/config/header-includes.yaml --pdf-engine=xelatex -o "$file".pdf "$file" 2> /dev/null
done
zip output/archived.zip output/*.pdf > /dev/null
