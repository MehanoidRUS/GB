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


        /// <summary>
        /// Перегрузка метода рисования
        /// </summary>
        public override void Draw()
        {            
            Game.Buffer.Graphics.DrawLine(colorStar, pos.X,pos.Y,pos.X + size.Width, pos.Y + size.Height);
            Game.Buffer.Graphics.DrawLine(colorStar, pos.X + size.Width, pos.Y, pos.X, pos.Y + size.Height);            
        }

        /// <summary>
        /// Обновление состояния объекта
        /// </summary>
        public override void Update<Star>(ref Star obj)
        {
            pos.X = pos.X - speed;
        }
        /// <summary>
        /// Задает цвет объекта при создание
        /// </summary>
        void ColorizeStars()
        {
            Color color = Color.FromArgb(Game.Rnd.Next(100, 200), Game.Rnd.Next(100, 200), Game.Rnd.Next(100, 200));
            this.colorStar = new Pen(color, 5);
        }
    }
}
