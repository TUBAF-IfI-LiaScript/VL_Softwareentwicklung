public class FullTimeEmployee{
    public int ID {get; set;}
    public string FirstName  {get; set;}
    public string LastName  {get; set;}
    public int AnnualSalary {get; set;}
    public string GetFullName(){
        return this.FirstName + " " + this.LastName;
    }
    public int GetMonthlySalary(){
        return this.AnnualSalary / 12;
    }
}
