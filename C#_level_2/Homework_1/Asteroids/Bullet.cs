using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Bullet : BaseObject,ICollision
    {
        Bitmap image;
        public Bullet(Bitmap image) 
        {
            this.image = image;
            this.pos = new Point(0, Game.Rnd.Next(0, Game.Height));            
            speed = 10;
        }

        public bool Collision(ICollision obj) => obj.Rect.IntersectsWith(this.Rect);

        public Rectangle Rect => new Rectangle(pos, image.Size);

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, pos.X, pos.Y);
        }
        /// <summary>
        /// Пересоздает объект в начальной точке
        /// </summary>
        public override void ReCreation()
        {
            this.pos.X = 0;
            this.pos.Y = Game.Rnd.Next(0, Game.Height);
        }

        public override void Update()
        {
            pos.X = pos.X + speed;
            if (pos.X>Game.Width-size.Width)
            {
                ReCreation();
            }
        }
    }
}
