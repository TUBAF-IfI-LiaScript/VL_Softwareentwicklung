public enum EmployeeType {FullTimeEmployee, ContractEmployee};

public class Employee{

    public EmployeeType typeName {get; set;}
    public int ID {get; set;}
    public string FirstName  {get; set;}
    public string LastName  {get; set;}

    // This information is relevant for FullTime employees only!
    public int AnnualSalary {get; set;}
     // This information is relevant for Contract employees only!
    public int HourlyPay {get; set;}
    public int TotalHoursWorked{get; set;}

    public string GetFullName(){
        return this.FirstName + " " + this.LastName;
    }

    public int GetMonthlySalary(){
        int result = 0;
        if (typeName==EmployeeType.FullTimeEmployee) result = this.AnnualSalary / 12;
        if (typeName==EmployeeType.ContractEmployee) result =  this.HourlyPay * this.TotalHoursWorked;
        return result;
    }

}