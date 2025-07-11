namespace CalcService;

public class Calculator
{
    public static int DivideTwoValues(double x, double y, ref double result)
    {
        if (y != 0)
        {
            result = x / y;
            return 0;
        }
        else return -1;
    }
    
    // Einfachere Methode für Python-Integration
    public static DivisionResult Divide(double x, double y)
    {
        if (y != 0)
        {
            return new DivisionResult { Success = true, Result = x / y };
        }
        return new DivisionResult { Success = false, Result = 0 };
    }
}

public class DivisionResult
{
    public bool Success { get; set; }
    public double Result { get; set; }
}