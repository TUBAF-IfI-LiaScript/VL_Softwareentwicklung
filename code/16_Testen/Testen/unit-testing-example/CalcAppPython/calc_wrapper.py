"""
CalcService Wrapper Modul für die Integration der C# Bibliothek.
"""
from pathlib import Path
from typing import Tuple

try:
    import pythonnet
    
    # Setze die .NET Core Runtime explizit
    try:
        pythonnet.set_runtime("coreclr")
    except:
        # Falls das fehlschlägt, versuche die Standard-Runtime (mono)
        pass
        
    import clr
except ImportError as exc:
    raise ImportError("pythonnet ist nicht installiert. Führe 'poetry install' aus.") from exc


class CalcServiceWrapper:
    """Wrapper-Klasse für die C# CalcService Bibliothek."""
    
    def __init__(self):
        """Initialisiert den Wrapper und lädt die C# Assembly."""
        self._calculator = None
        self._load_assembly()
    
    def _load_assembly(self):
        """Lädt die CalcService Assembly."""
        current_dir = Path(__file__).parent
        calc_service_path = current_dir.parent / "CalcService" / "bin" / "Debug" / "net9.0"
        dll_path = calc_service_path / "CalcService.dll"
        
        if not dll_path.exists():
            raise FileNotFoundError(
                f"CalcService.dll nicht gefunden unter {dll_path}\n"
                "Bitte baue zuerst die C# Lösung mit 'dotnet build'"
            )
        
        try:
            # Füge den Assembly-Pfad zum System.AppDomain hinzu
            import sys
            sys.path.append(str(calc_service_path))
            
            # Lade die Assembly über den Namen
            clr.AddReference("CalcService")
            
            from CalcService import Calculator
            self._calculator = Calculator
        except Exception as e:
            raise RuntimeError(f"Fehler beim Laden der Assembly: {e}") from e
    
    def divide(self, x: float, y: float) -> Tuple[bool, float]:
        """
        Führt eine Division durch.
        
        Args:
            x: Dividend
            y: Divisor
            
        Returns:
            Tupel (success, result) - success ist True wenn erfolgreich, 
            result ist das Ergebnis der Division
        """
        if self._calculator is None:
            raise RuntimeError("Calculator nicht geladen")
        
        try:
            # Verwende die neue einfachere Divide-Methode
            result = self._calculator.Divide(x, y)
            return result.Success, float(result.Result)
        except Exception as e:
            raise RuntimeError(f"Fehler bei der Division: {e}") from e
    
    def is_available(self) -> bool:
        """Prüft ob die CalcService verfügbar ist."""
        return self._calculator is not None
