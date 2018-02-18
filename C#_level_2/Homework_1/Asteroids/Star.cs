using System;
using System.Drawing;

namespace Asteroids
{
    class Star: BaseObject
    {
        Pen colorStar;
        public Star(Size size):base(size)
        {
            speed = Game.Rnd.Next(1, 10);
            ColorizeStars();
        }

        //Метод задает новые параметры объекта
        public override void ReCreation()
        {
            this.pos.X = Game.Width+5;
            this.pos.Y=Game.Rnd.Next(0, Game.Height);
            ColorizeStars();

        }

        public override void Draw()
        {            
            Game.Buffer.Graphics.DrawLine(colorStar, pos.X,pos.Y,pos.X + size.Width, pos.Y + size.Height);
            Game.Buffer.Graphics.DrawLine(colorStar, pos.X + size.Width, pos.Y, pos.X, pos.Y + size.Height);            
        }
        public override void Update()
        {
            pos.X = pos.X - speed;
            if (pos.X<0)
            {
                ReCreation();
            }            
        }
        //Задает различный цвет звезд
        void ColorizeStars()
        {
            Color color = Color.FromArgb(Game.Rnd.Next(100, 200), Game.Rnd.Next(100, 200), Game.Rnd.Next(100, 200));
            this.colorStar = new Pen(color, 5);
        }
    }
}
