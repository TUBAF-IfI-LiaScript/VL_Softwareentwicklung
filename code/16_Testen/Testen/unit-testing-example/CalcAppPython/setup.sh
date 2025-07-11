#!/bin/bash
# Setup-Skript für CalcApp Python

echo "=== CalcApp Python Setup ==="
echo "Dieses Skript richtet die Python-Umgebung ein und baut die C# Abhängigkeiten."
echo

# Prüfe ob Poetry installiert ist
if ! command -v poetry &> /dev/null; then
    echo "❌ Poetry ist nicht installiert!"
    echo "Installiere Poetry mit: curl -sSL https://install.python-poetry.org | python3 -"
    exit 1
fi

echo "✅ Poetry gefunden"

# Wechsle in das CalcAppPython Verzeichnis
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"

echo "📁 Arbeitsverzeichnis: $SCRIPT_DIR"

# Installiere Python-Abhängigkeiten
echo "📦 Installiere Python-Abhängigkeiten..."
poetry install --no-root

if [ $? -ne 0 ]; then
    echo "❌ Fehler beim Installieren der Python-Abhängigkeiten"
    exit 1
fi

echo "✅ Python-Abhängigkeiten installiert"

# Baue C# CalcService
echo "🔨 Baue C# CalcService..."
cd "../CalcService"
dotnet build

if [ $? -ne 0 ]; then
    echo "❌ Fehler beim Bauen der C# CalcService"
    exit 1
fi

echo "✅ C# CalcService erfolgreich gebaut"

# Zurück zum Python-Verzeichnis
cd "$SCRIPT_DIR"

echo
echo "🎉 Setup abgeschlossen!"
echo
echo "Du kannst nun die Anwendung starten:"
echo "  poetry run python main.py     # Interaktive Anwendung"
echo "  poetry run python demo.py     # Demo-Modus"
echo "  poetry run python test_calc.py # Tests ausführen"
echo
