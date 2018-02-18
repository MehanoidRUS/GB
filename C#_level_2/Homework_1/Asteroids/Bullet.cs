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
        public Bullet() 
        {
            this.pos = new Point(0, Game.Rnd.Next(0, Game.Height));
            this.size = new Size(10,5);
            speed = 10;
        }

        public Rectangle Rect => new Rectangle(pos,size);

        public bool Collision(ICollision obj) => obj.Rect.IntersectsWith(this.Rect);

        public override void Draw()
        {
            Game.Buffer.Graphics.FillRectangle(Brushes.OrangeRed,pos.X,pos.Y,size.Width,size.Height);
        }

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
