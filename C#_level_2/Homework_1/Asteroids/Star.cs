using System;
using System.Drawing;

namespace Asteroids
{
    class Star: BaseObject
    {
        public Star(Point pos,Point dir, Size size):base(pos,dir,size)
        {

        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.DarkBlue, pos.X,pos.Y,pos.X + size.Width, pos.Y + size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.DarkBlue, pos.X + size.Width, pos.Y, pos.X, pos.Y + size.Height);
        }
        public override void Update()
        {
            pos.X = pos.X - dir.X;
            if (pos.X<0)
            {
                pos.X = Game.Width + size.Width;
            }
            
        }
    }
}
