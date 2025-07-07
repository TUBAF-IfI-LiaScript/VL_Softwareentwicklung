#!/usr/bin/env python3
"""
Demo-Skript für die CalcApp Python Anwendung.
"""
from calc_wrapper import CalcServiceWrapper


def run_demo():
    """Führt eine kurze Demo der CalcService Integration aus."""
    print("=" * 60)
    print("DEMO: CalcApp Python - C# CalcService Integration")
    print("=" * 60)
    
    try:
        # CalcService laden
        print("\n1. Lade CalcService Assembly...")
        calc = CalcServiceWrapper()
        
        if calc.is_available():
            print("   ✅ CalcService erfolgreich geladen!")
        else:
            print("   ❌ Fehler beim Laden der CalcService")
            return
        
        # Verschiedene Berechnungen durchführen
        print("\n2. Führe verschiedene Berechnungen durch:")
        
        test_cases = [
            (20.0, 4.0),
            (100.0, 7.0),
            (45.5, 2.5),
            (10.0, 0.0),  # Division durch Null
            (-15.0, 3.0)
        ]
        
        for i, (x, y) in enumerate(test_cases, 1):
            print(f"\n   Test {i}: {x} ÷ {y}")
            
            success, result = calc.divide(x, y)
            
            if success:
                print(f"   → Ergebnis: {result:.6f}")
            else:
                print("   → Fehler: Division durch Null")
        
        print("\n3. Demo abgeschlossen!")
        print("   ✅ Alle Tests erfolgreich durchgeführt")
        
        print("\n" + "=" * 60)
        print("Die C# CalcService Bibliothek wurde erfolgreich")
        print("von Python über pythonnet verwendet!")
        print("=" * 60)
        
    except Exception as e:
        print(f"\n❌ Fehler in der Demo: {e}")


if __name__ == "__main__":
    run_demo()
