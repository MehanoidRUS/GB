using System;
using System.Drawing;

namespace Asteroids
{
    class Ship : BaseObject, ICollision
    {
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

        public Rectangle Rect => new Rectangle(pos, SizeObject);
        public bool Collision(ICollision obj) => obj.Rect.IntersectsWith(this.Rect);

        

        /// <summary>
        /// Метод движения вверх
        /// </summary>
        public void Up()
        {
            if (pos.Y > 0)
            {
                pos.Y -= speed;
            }
        }

        /// <summary>
        /// Метод движения вниз
        /// </summary>
        public void Down()
        {
            if (pos.Y < (Game.Height-size.Height))
            {
                pos.Y += speed;
            }
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, this.pos);
        }

        public static event Message MessageDie;


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
        }
    }
}
