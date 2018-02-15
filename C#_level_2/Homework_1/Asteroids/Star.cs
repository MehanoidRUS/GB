using System;
using System.Drawing;

namespace Asteroids
{
    class Star: BaseObject
    {
        Pen colorStar;
        public Star(Size size):base(size)
        {
            ColorizeStars();
        }
        public override void Draw()
        {            
            Game.Buffer.Graphics.DrawLine(colorStar, pos.X,pos.Y,pos.X + size.Width, pos.Y + size.Height);
            Game.Buffer.Graphics.DrawLine(colorStar, pos.X + size.Width, pos.Y, pos.X, pos.Y + size.Height);            
        }
        public override void Update()
        {
            pos.X = pos.X - dir.X;
            if (pos.X<0)
            {
                pos.X = Game.Width + size.Width;
            }            
        }

        void ColorizeStars()
        {
            Color color = Color.FromArgb(Game.Rnd.Next(100, 200), Game.Rnd.Next(100, 200), Game.Rnd.Next(100, 200));
            this.colorStar = new Pen(color, 2);
        }
    }
}
