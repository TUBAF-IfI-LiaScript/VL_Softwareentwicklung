# CalcApp Python

Eine Python-Anwendung, die die C# CalcService Bibliothek über pythonnet nutzt und Divisionsberechnungen durchführt.

## 🚀 Quick Start

### Automatisches Setup
```bash
./setup.sh
```

### Manuelles Setup

1. **Poetry installieren** (falls noch nicht vorhanden):
   ```bash
   curl -sSL https://install.python-poetry.org | python3 -
   ```

2. **Python-Abhängigkeiten installieren**:
   ```bash
   poetry install --no-root
   ```

3. **C# CalcService bauen**:
   ```bash
   cd ../CalcService
   dotnet build
   cd ../CalcAppPython
   ```

## 📋 Anwendungen

### Demo ausführen
```bash
poetry run python demo.py
```

### Interaktive Anwendung
```bash
poetry run python main.py
```

### Tests ausführen
```bash
poetry run python test_calc.py
```

## 🔧 Funktionalität

Die Anwendung demonstriert:

- **C#/.NET Integration**: Nutzung einer C# Bibliothek aus Python
- **Poetry Dependency Management**: Moderne Python-Paketverwaltung
- **Error Handling**: Robuste Behandlung von Division durch Null
- **Interaktive CLI**: Benutzerfreundliche Kommandozeilenschnittstelle

### Verfügbare Methoden

- `Calculator.Divide(x, y)` - Einfache Division mit Erfolgs-/Fehler-Rückgabe
- `Calculator.DivideTwoValues(x, y, ref result)` - Original C# Methode mit ref-Parameter

## 📁 Projektstruktur

```
CalcAppPython/
├── pyproject.toml          # Poetry-Konfiguration
├── README.md               # Diese Datei
├── setup.sh               # Automatisches Setup-Skript
├── .gitignore             # Git-Ignore-Regeln
├── main.py                # Haupt-Anwendung (interaktiv)
├── demo.py                # Demo-Modus
├── test_calc.py           # Test-Suite
├── calc_wrapper.py        # C# CalcService Wrapper
└── __init__.py           # Python-Paket-Definition
```

## 🛠️ Technologie-Stack

- **Python 3.8-3.12**: Basis-Programmiersprache
- **Poetry**: Dependency Management und Packaging
- **pythonnet**: .NET/C# Integration
- **C# .NET 9**: CalcService Bibliothek

## 🔍 Beispiel-Nutzung

```python
from calc_wrapper import CalcServiceWrapper

# CalcService initialisieren
calc = CalcServiceWrapper()

# Division durchführen
success, result = calc.divide(10.0, 2.0)

if success:
    print(f"Ergebnis: {result}")  # Ausgabe: Ergebnis: 5.0
else:
    print("Fehler bei der Division")
```

## 🚨 Troubleshooting

### Fehler: "Could not find libmono"
- Stelle sicher, dass .NET Core/5+ installiert ist
- Die Anwendung ist für .NET Core/5+ konfiguriert, nicht Mono

### Fehler: "CalcService.dll nicht gefunden"
- Führe `dotnet build` im CalcService Verzeichnis aus
- Prüfe ob die DLL unter `../CalcService/bin/Debug/net9.0/` existiert

### Fehler: "pythonnet ist nicht installiert"
- Führe `poetry install --no-root` aus
- Stelle sicher, dass Poetry korrekt installiert ist

## 📈 Erweiterungsmöglichkeiten

- Weitere mathematische Operationen hinzufügen
- GUI mit tkinter oder PyQt entwickeln
- Web-API mit FastAPI erstellen
- Unit-Tests mit pytest erweitern
