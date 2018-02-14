using System;
using System.Windows.Forms;
using System.Drawing;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Asteroids
{
    static class Game
    {
        private static BufferedGraphicsContext gameContext;
        public static BufferedGraphics Buffer;
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static BaseObject[] BaseObjectArray;
        

        static Game()
        {

        }

        public static void Init(Form form)
        {
            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
            Graphics graphics;
            gameContext = BufferedGraphicsManager.Current;
            graphics = form.CreateGraphics();
            Width = form.Width;
            Height = form.Height;
            Buffer = gameContext.Allocate(graphics, new Rectangle(0, 0, Width, Height));
            Load();
        }

        public static void Load()
        {
            BaseObjectArray = new BaseObject[30];
            for (int i = 0; i < BaseObjectArray.Length/2; i++)
            {
                BaseObjectArray[i] = new Star(new Point(600, i * 20), new Point(15 - i, 15 - i), new Size(20, 20));
            }
            for (int i = BaseObjectArray.Length / 2; i < BaseObjectArray.Length; i++)
            {
                BaseObjectArray[i] = new BaseObject(new Point(600, i * 20), new Point(15 - i, 15 - i), new Size(20, 20));
            }
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (var obj in BaseObjectArray)
            {
                obj.Draw();
            }
            Buffer.Render();
        }

        public static void Update()
        {
            foreach (var obj in BaseObjectArray)
            {
                obj.Update();
            }
        }
        private static void Timer_Tick(object sender,EventArgs e)
        {
            Draw();
            Update();
        }
    }
}
