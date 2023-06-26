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
            btn.Click += new EventHandler(onClick);
            add += new DelEventHandler(Initiate);

            TextBox Mytextbox = new TextBox();
                    Mytextbox.Location = new Point(187, 51);
                    Mytextbox.BackColor = Color.LightGray;
                    Mytextbox.ForeColor = Color.DarkOliveGreen;
                    Mytextbox.AutoSize = true;
                    Mytextbox.Name = "text_box1";
                    Mytextbox.Text = "Sorry running Update!";




            //invoke the event
            add();
        }
        //call when event is fired
        public void Initiate()
        {
            MessageBox.Show("Initiate programm");
        }

        //call when button clicked
        public void onClick(object sender, EventArgs e)
        {
            MessageBox.Show("onClick");
        }
        static void Main(string[] args)
        {
            Application.Run(new Program());

            Console.ReadLine();
        }
    }
}
