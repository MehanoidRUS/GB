using System;
using System.Drawing;

namespace Asteroids
{
    /*Класс реализации персонажа*/
    class Ship : BaseObject
    {
        //Колличесвто жизней
        private int live = 3;
        //Изображение персонажа
        Bitmap image= new Bitmap(@"Image\ship.png");

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Ship()
        {
            this.pos = new Point(0,Game.Height/2);
            this.size = image.Size;
            speed = 1;
        }

        /// <summary>
        /// Свойство, возвращает координаты корабля
        /// </summary>
        public Point ShipPosition => pos;

        /// <summary>
        /// Метод возвращает колличество жизней
        /// </summary>
        public int Live => live;
        /// <summary>
        /// Метод уменьшает кол-во жизней на 1
        /// </summary>
        public int Damage => live--;

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, pos.X, pos.Y);
        }

        /// <summary>
        /// Перемещение объекта вверх
        /// </summary>
        public void Up()
        {
            pos.Y += speed;
        }
        /// <summary>
        /// Перемещение объекта вниз
        /// </summary>
        public void Down()
        {
            pos.Y -= speed;
        }

        public override void ReCreation()
        {
        }

        public override void Update()
        {
        }
    }
}
