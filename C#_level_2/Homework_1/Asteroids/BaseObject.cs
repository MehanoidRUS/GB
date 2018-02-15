using System;
using System.Drawing;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Asteroids
{
    class BaseObject
    {
        protected Point pos;
        protected Point dir;
        protected Size size;

        public BaseObject(Size size):this()
        {            
            this.size = size;
        }

        public BaseObject()
        {
            this.pos = new Point(Game.Width + 10, Game.Rnd.Next(0, Game.Height));
            this.dir = new Point(Game.Rnd.Next(2,10), Game.Rnd.Next(1, 20));
        }

        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.White, pos.X, pos.Y, size.Width, size.Height);
        }
        public virtual void Update()
        {
            pos.X = pos.X + dir.X;
            pos.Y = pos.Y + dir.Y;
            if (pos.X<0)
            {
                dir.X = -dir.X;
            }
            if (pos.X>Game.Width)
            {
                dir.X = -dir.X;
            }
            if (pos.Y < 0)
            {
                dir.Y = -dir.Y;
            }
            if (pos.Y > Game.Height)
            {
                dir.Y = -dir.Y;
            }
        }

    }
}
