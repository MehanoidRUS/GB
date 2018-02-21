using System;
using System.Drawing;

namespace Asteroids
{
    class Ship : BaseObject
    {
        private int live = 3;

        public Ship(Point point)
        {
            this.image= new Bitmap(@"Image\ship.png");
            this.size= this.image.Size;
            Console.WriteLine($"Game.Height={Game.Height}");
            this.pos = point;
            Console.WriteLine($"ship in {this.pos}");
            Console.WriteLine($"ship in {this.size}");

        }
        /// <summary>
        /// Метод возвращает колличество жизней
        /// </summary>
        public int Live => live;

        /// <summary>
        /// Метод обрабатывающий полученный урон
        /// </summary>
        public int Damage => live--;

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, this.pos);
        }

        public override void ReCreation()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
