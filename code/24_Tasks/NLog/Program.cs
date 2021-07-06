// Import log4net classes.
using log4net;
using log4net.Config;

public class Bar 
{
    private static readonly ILog log = LogManager.GetLogger(typeof(Bar));
    public void DoIt()
    {
        log.Debug("Did it again!");
    }
}
public class MyApp 
{
    // Define a static logger variable so that it references the
    // Logger instance named "MyApp".
    private static readonly ILog log = LogManager.GetLogger(typeof(MyApp));

    static void Main(string[] args) 
    {
        // Set up a simple configuration that logs on the console.
        BasicConfigurator.Configure();

        log.Info("Entering application.");
        log.Error("Hier passiert irgendwas");
        Bar bar = new Bar();
        bar.DoIt();
        log.Info("Exiting application.");
    }
}
