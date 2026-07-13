public class StudentModel
{
    private string _name { get; set; }
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public StudentModel(string name)
    {
        Name = name;
    }

    public string ToUpper()
    {
        return Name.ToUpper();
    }
}
