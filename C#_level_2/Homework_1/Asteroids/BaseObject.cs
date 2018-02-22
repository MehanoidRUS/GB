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

        public delegate void Message();

        /// <summary>
        /// Свойство возвращает размер объекта
        /// </summary>
        public Size SizeObject => size;
        protected BaseObject(Size size) : this()
        {
            this.size = size;
        }

        protected BaseObject()
        {
            this.pos = new Point(Game.Width + 10, Game.Rnd.Next(0, Game.Height));
            this.dir = new Point(Game.Rnd.Next(2, 10), Game.Rnd.Next(1, 20));
        }
        public Point Position => pos;

        public abstract void Draw();

        /// <summary>
        /// Обновление состояния объекта
        /// </summary>
        public abstract void Update<T>(ref T obj);
        /// <summary>
        /// Удаляет объект
        /// </summary>
        protected void Destroy<T>(ref T obj)
        {
           obj = default(T);
        }
    }
}
