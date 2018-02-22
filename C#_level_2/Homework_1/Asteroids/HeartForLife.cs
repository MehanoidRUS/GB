using System;
using System.Drawing;

namespace Asteroids
{
    class HeartForLife : BaseObject,ICollision
    {
        /// <summary>
        /// Создает объект с параметрами по-умолчанию
        /// </summary>
        public HeartForLife()
        {
            this.image= new Bitmap(@"Image\live.png");
            this.size = image.Size;
            this.pos = new Point(Game.Width, 400);
            //this.pos = new Point(400,400);
            this.speed =10;
        }

        public Rectangle Rect => new Rectangle(pos,SizeObject);

        public bool Collision(ICollision obj) => obj.Rect.IntersectsWith(this.Rect);
        

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, pos.X, pos.Y);
        }

        /// <summary>
        /// Обновление состояния объекта
        /// </summary>
        public override void Update<HeartForLife>(ref HeartForLife obj)
        {
            pos.X -= speed;
 
        }
    }
}
