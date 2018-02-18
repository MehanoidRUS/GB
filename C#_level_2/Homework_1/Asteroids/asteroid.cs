using System;
using System.Drawing;


namespace Asteroids
{
    class Asteroid:BaseObject,ICollision
    {
        Bitmap image;
        public Asteroid():base()
        {
            image = new Bitmap(Properties.Resources.asteroid64x64);            
            this.dir= new Point(Game.Rnd.Next(3, 10), Game.Rnd.Next(5));
        }

        public Rectangle Rect => new Rectangle(pos,image.Size);

        public bool Collision(ICollision obj) => obj.Rect.IntersectsWith(this.Rect);

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, pos.X, pos.Y);
            
        }

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
