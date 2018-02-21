using System;
using System.Drawing;


namespace Asteroids
{
    abstract class BaseObject
    {
        protected Bitmap image;
        protected Point pos;
        protected Point dir;
        protected int speed = 0;
        protected Size size;
        /// <summary>
        /// Свойство возвращает размер объекта
        /// </summary>
        public Size SizeObject => size;
        protected BaseObject(Size size):this()
        {            
            this.size = size;
        }

        protected BaseObject()
        {
            this.pos = new Point(Game.Width + 10, Game.Rnd.Next(0, Game.Height));
            this.dir = new Point(Game.Rnd.Next(2,10), Game.Rnd.Next(1, 20));
        }

        public abstract void Draw();

        public abstract void Update();
        /// <summary>
        /// Пересоздает объект в начальной точке
        /// </summary>
        public abstract void ReCreation();

    }
}
