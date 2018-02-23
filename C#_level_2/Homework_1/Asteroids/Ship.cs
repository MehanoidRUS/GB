using System;
using System.Drawing;

namespace Asteroids
{
    class Ship : BaseObject, ICollision
    {
        //Количество жизней
        private int live = 3;

        public Ship()
        {
            this.image = new Bitmap(@"Image\ship.png");
            this.size = this.image.Size;
            this.pos = new Point(0, Game.Height / 2);
            this.speed = 10;
        }

        /// <summary>
        /// Возвращает координаты корабля
        /// </summary>
        public Point ShipPosition => pos;

        /// <summary>
        /// Возвращает размер объекта
        /// </summary>
        public Size Size => size;

        /// <summary>
        /// Метод возвращает колличество жизней
        /// </summary>
        public int Live => live;

        /// <summary>
        /// Метод обрабатывающий полученный урон
        /// </summary>
        public void Damage() => live--;

        /// <summary>
        /// Метод восстановления здоровтя
        /// </summary>
        public void Heal()
        {
            if (live < 3) live++;
        }

        /// <summary>
        /// Реализация интерфейса ICollision
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Collision(ICollision obj) => obj.Rect.IntersectsWith(this.Rect);
        public Rectangle Rect => new Rectangle(pos, size);


        /// <summary>
        /// Метод движения вверх
        /// </summary>
        public void Up()
        {
         pos.Y -= speed;
        }

        /// <summary>
        /// Метод движения вниз
        /// </summary>
        public void Down()
        { 
            pos.Y += speed;
        }

        /// <summary>
        /// Перегрузка метода рисования
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, this.pos);
        }

        /// <summary>
        /// Происходит при live=0
        /// </summary>
        public static event Action MessageDie;


        /// <summary>
        /// Метод запускающий событие при live=0
        /// </summary>
        public void Die()
        {
            MessageDie?.Invoke();
        }

        /// <summary>
        /// Обновление состояния объекта
        /// </summary>
        public override void Update<Ship>(ref Ship obj)
        {
            if (pos.Y<0)
            {
                pos.Y = Game.Height - size.Height;
            }
            if (pos.Y >(Game.Height - size.Height))
            {
                pos.Y = 0;
            }
        }
    }
}
