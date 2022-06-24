using System;  
using System.Drawing;  
using System.Windows.Forms; 
using System.Diagnostics; 
  
namespace Delegates  
{  
    //custom delegate  
    public delegate void DelEventHandler();  
  
    class Program :Form   
    {  
        //custom event  
        public event DelEventHandler add;  
  
        public Program()  
        {  
            // desing a button over form  
            Button btn = new Button();  
            btn.Parent = this;  
            btn.Text = "Hit Me";  
            btn.Location = new Point(100,100);  
  
            //Event handler is assigned to  
            // the button click event  
            btn.Click += new EventHandler(onClcik);  
            add += new DelEventHandler(Initiate);  
  
            //invoke the event  
            add();  
        }  
        //call when event is fired  
        public void Initiate()  
        {  
            MessageBox.Show("Initiate programm");  
        }  
   
        //call when button clicked  
        public void onClcik(object sender, EventArgs e)  
        {  
            MessageBox.Show("onClcik");  
        }  
        static void Main(string[] args)  
        {  
            Application.Run(new Program());  
   
            Console.ReadLine();  
        }  
    }  
}
