public class ContractEmployee{
    public int ID {get; set;}
    public string FirstName  {get; set;}
    public string LastName  {get; set;}
    public int HourlyPay {get; set;}
    public int TotalHoursWorked{get; set;}
    public string GetFullName(){
        return this.FirstName + " " + this.LastName;
    }
    public int GetMonthlySalary(){
        return this.HourlyPay * this.TotalHoursWorked;
    }
}
