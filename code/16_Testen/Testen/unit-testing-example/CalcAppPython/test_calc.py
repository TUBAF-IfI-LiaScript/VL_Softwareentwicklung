#!/usr/bin/env python3
"""
Einfaches Test-Skript für die CalcService Integration.
"""
from calc_wrapper import CalcServiceWrapper


def test_calc_service():
    """Testet die grundlegende Funktionalität."""
    print("=== CalcService Test ===")
    
    try:
        # Wrapper initialisieren
        calc = CalcServiceWrapper()
        
        if not calc.is_available():
            print("❌ CalcService nicht verfügbar")
            return False
        
        print("✅ CalcService erfolgreich geladen")
        
        # Test-Fälle
        test_cases = [
            (10.0, 2.0, 5.0),    # Normale Division
            (15.0, 3.0, 5.0),    # Normale Division
            (7.0, 0.0, None),    # Division durch Null
        ]
        
        all_passed = True
        
        for x, y, expected in test_cases:
            success, result = calc.divide(x, y)
            
            if y == 0.0:
                # Erwarten Fehler
                if not success:
                    print(f"✅ {x} ÷ {y} = Fehler (erwartet)")
                else:
                    print(f"❌ {x} ÷ {y} = {result} (Fehler erwartet)")
                    all_passed = False
            else:
                # Erwarten Erfolg
                if success and abs(result - expected) < 0.001:
                    print(f"✅ {x} ÷ {y} = {result}")
                else:
                    print(f"❌ {x} ÷ {y} = {result} (erwartet: {expected})")
                    all_passed = False
        
        return all_passed
        
    except Exception as e:
        print(f"❌ Fehler beim Test: {e}")
        return False


if __name__ == "__main__":
    success = test_calc_service()
    print(f"\nTest {'✅ BESTANDEN' if success else '❌ FEHLGESCHLAGEN'}")
