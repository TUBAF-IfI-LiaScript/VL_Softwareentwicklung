public class BaseEmployee{
    public int ID {get; set;}
    public string FirstName  {get; set;}
    public string LastName  {get; set;}

    public string GetFullName(){
        return this.FirstName + " " + this.LastName;
    }

    public virtual int GetMonthlySalary(){
      throw new NotImplementedException();
    }
}
