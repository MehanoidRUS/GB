using System;
using System.Drawing;

namespace Asteroids
{
    class Star: BaseObject
    {
        Pen colorStar;
        /// <summary>
        /// Создает объект Star 
        /// </summary>
        /// <param name="size">Размер объекта</param>
        /// <param name="moving">Должен ли объект перемещаться</param>
        public Star(Size size,bool moving):base(size)
        {
            if (moving)
            {
                speed = Game.Rnd.Next(1, 10);
            }
            else
            {
                pos = new Point(Game.Rnd.Next(1, Game.Width), Game.Rnd.Next(1, Game.Height));
            }
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
        protected void ColorizeStars()
        {
            Color color = Color.FromArgb(Game.Rnd.Next(100, 200), Game.Rnd.Next(100, 200), Game.Rnd.Next(100, 200));
            this.colorStar = new Pen(color, 5);
        }
    }
}
