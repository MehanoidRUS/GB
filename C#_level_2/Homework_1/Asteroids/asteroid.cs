using System;
using System.Drawing;

//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Asteroids
{
    class Asteroid:BaseObject
    {
        Bitmap image;
        public Asteroid():base()
        {
            image = new Bitmap(Properties.Resources.asteroid64x64);            
            this.dir= new Point(Game.Rnd.Next(3, 10), Game.Rnd.Next(5));
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, pos.X, pos.Y);
        }
        public override void Update()
        {
            pos.X = pos.X - dir.X;
            pos.Y = pos.Y + dir.Y;
            if (pos.X < 0)
            {
                pos.X = Game.Width+10;
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
