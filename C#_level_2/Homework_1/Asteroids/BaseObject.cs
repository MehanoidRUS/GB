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

        protected BaseObject(Size size) : this()
        {
            this.size = size;
        }

        protected BaseObject()
        {
            this.pos = new Point(Game.Width + 10, Game.Rnd.Next(0, Game.Height));
            this.dir = new Point(Game.Rnd.Next(2, 10), Game.Rnd.Next(1, 20));
        }

        /// <summary>
        /// Возвращает позицию объекта
        /// </summary>
        public Point Position => pos;
            
        /// <summary>
        /// Абстрактный метод рисования
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Обновление состояния объекта
        /// </summary>
        public abstract void Update<T>(ref T obj);

        /// <summary>
        /// Удаляет объект
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">Ссылка на объект</param>
        protected void Destroy<T>(ref T obj)
        {
           obj = default(T);
        }
    }
}
