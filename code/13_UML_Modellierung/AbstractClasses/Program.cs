// Welche Member sind in den beiden Klassen FullTimeEmployee und
// ContractEmployee mehrfach implementiert. Extrahieren Sie diese in einer
// separaten Basisklasse, lassen sie die bestehenden Methoden davon erben.

// Definieren Sie für GetMonthlySalary eine virtuelle Methode in der Basisklasse
// die in den abgeleiteten Klassen überschrieben wird.

// Wie können Sie vermeiden, dass die Basisklasse instanziert wird.?

using System;

class Program
{
    public static void Main(){

        FullTimeEmployee fte = new FullTimeEmployee()
        {
            ID = 123,
            FirstName = "Mark",
            LastName = "Zuckerberg",
            AnnualSalary = 200000
        };

        Console.WriteLine(fte.GetFullName());
        Console.WriteLine(fte.GetMonthlySalary());

        ContractEmployee cte = new ContractEmployee()
        {
            ID = 345,
            FirstName = "Charles",
            LastName = "Babbage",
            HourlyPay = 200,
            TotalHoursWorked = 40
        };

        Console.WriteLine(cte.GetFullName());
        Console.WriteLine(cte.GetMonthlySalary());
    }
}
