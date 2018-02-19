using System;
using System.Windows.Forms;

namespace Asteroids
{
    class Program
    {
        static void Main()
        {
            Form form = MainForm.CreateForm(8000, -600);
            Game.Init(form);
            form.Show();
            Game.Draw();
            Application.Run(form);                     
        }
    }
}
