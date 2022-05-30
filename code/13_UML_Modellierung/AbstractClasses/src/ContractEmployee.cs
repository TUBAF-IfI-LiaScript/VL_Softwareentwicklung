public class ContractEmployee : BaseEmployee{
    public int HourlyPay {get; set;}
    public int TotalHoursWorked{get; set;}
    public string GetFullName(){
        return this.FirstName + " " + this.LastName;
    }
    public int GetMonthlySalary(){
        return this.HourlyPay * this.TotalHoursWorked;
    }
}
