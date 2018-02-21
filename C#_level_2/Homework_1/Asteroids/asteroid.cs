using System;
using System.Drawing;


namespace Asteroids
{
    class Asteroid:BaseObject,ICollision
    {
        
        Bitmap image;
        public Asteroid(Bitmap image):base()
        {
            this.image = image;
            this.size = image.Size;
            this.dir= new Point(Game.Rnd.Next(3, 10), Game.Rnd.Next(5));
        }

        public Rectangle Rect => new Rectangle(pos,SizeObject);

        public bool Collision(ICollision obj) => obj.Rect.IntersectsWith(this.Rect);

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, pos.X, pos.Y);
            
        }

        /// <summary>
        /// Пересоздает объект в начальной точке
        /// </summary>
        public override void ReCreation()
        {
            this.pos.X = Game.Width + 5;
            this.pos.Y = Game.Rnd.Next(0, Game.Height);
        }

        public override void Update()
        {
            pos.X = pos.X - dir.X;
            pos.Y = pos.Y + dir.Y;
            if (pos.X < 0)
            {
                ReCreation();
            }
            if (pos.Y < 0)
            {
                pos.Y = Game.Height;
            }
            if (pos.Y > Game.Height)
            {
                pos.Y = 0;
            }
        }
    }
}
