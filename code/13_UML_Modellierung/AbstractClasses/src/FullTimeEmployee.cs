public class FullTimeEmployee : BaseEmployee{
    public int AnnualSalary {get; set;}
    public string GetFullName(){
        return this.FirstName + " " + this.LastName;
    }
    public int GetMonthlySalary(){
        return this.AnnualSalary / 12;
    }
}
