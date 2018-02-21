using System;
using System.Windows.Forms;

namespace Asteroids
{
    class Program
    {
        static void Main()
        {
            MainForm form = new MainForm(800,600);
            Game.Init(form);
            form.Show();
            Game.Draw();
            Application.Run(form);                     
        }
    }
}
