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
            this.pos = new Point(Game.Width, Game.Rnd.Next(1,Game.Height-this.size.Height));
            //this.pos = new Point(400,400);
            this.speed =10;
        }

        /// <summary>
        /// Реализация интерфейса ICollision
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Collision(ICollision obj) => obj.Rect.IntersectsWith(this.Rect);
        public Rectangle Rect => new Rectangle(pos, this.size);

        /// <summary>
        /// Перегрузка метода рисования
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, this.pos.X, this.pos.Y);
        }

        /// <summary>
        /// Обновление состояния объекта
        /// </summary>
        public override void Update<HeartForLife>(ref HeartForLife obj)
        {
            this.pos.X -= speed;
            if (this.pos.X<0-this.size.Width)
            {
                Destroy(ref obj);
            }
        }
    }
}
