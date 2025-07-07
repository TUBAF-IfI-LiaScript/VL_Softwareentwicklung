#!/usr/bin/env python3
"""
Python-Anwendung die die C# CalcService Bibliothek über pythonnet nutzt.
"""
import sys
from calc_wrapper import CalcServiceWrapper


def demonstrate_division(calc_wrapper):
    """Demonstriert die Nutzung der Division-Methode."""
    print("=== CalcService Division Demo ===")
    
    # Test-Fälle
    test_cases = [
        (10.0, 2.0),
        (15.5, 3.1),
        (100.0, 0.0),  # Division durch Null
        (-20.0, 4.0),
        (7.0, -2.0)
    ]
    
    for x, y in test_cases:
        print(f"\nBerechnung: {x} ÷ {y}")
        
        try:
            success, result = calc_wrapper.divide(x, y)
            
            if success:
                print(f"Ergebnis: {result}")
            else:
                print("Fehler: Division durch Null nicht erlaubt")
                
        except Exception as e:
            print(f"Fehler bei der Berechnung: {e}")




def main():
    """Hauptfunktion der Anwendung."""
    print("CalcApp Python - C# CalcService Integration")
    print("=" * 50)
    
    try:
        # CalcService Wrapper initialisieren
        calc = CalcServiceWrapper()
        
        if not calc.is_available():
            print("❌ CalcService nicht verfügbar")
            sys.exit(1)
        
        print("✅ CalcService erfolgreich geladen")
    
        
    except Exception as e:
        print(f"❌ Fehler beim Initialisieren: {e}")
        sys.exit(1)


if __name__ == "__main__":
    main()
