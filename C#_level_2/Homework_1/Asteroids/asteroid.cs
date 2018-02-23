using System;
using System.Drawing;


namespace Asteroids
{
    class Asteroid:BaseObject,ICollision
    {
        public Asteroid(Bitmap image)
        {
            this.image = image;
            this.size = image.Size;
            this.dir= new Point(Game.Rnd.Next(3, 10), Game.Rnd.Next(5));
        }

        /// <summary>
        /// Реализация интерфейса ICollision
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Collision(ICollision obj) => obj.Rect.IntersectsWith(this.Rect);
        public Rectangle Rect => new Rectangle(pos, size);

        /// <summary>
        /// Перегрузка метода рисования
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, pos.X, pos.Y);
            
        }

        /// <summary>
        /// Обновление параметров объекта
        /// </summary>
        public override void Update<Asteroid>(ref Asteroid obj)
        {
            this.pos.X = pos.X - dir.X;
            pos.Y = pos.Y + dir.Y;
            if (pos.X<0)
            {
                Destroy<Asteroid>(ref obj);
            }

        }
    }
}
