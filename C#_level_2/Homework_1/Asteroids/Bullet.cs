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
        
        public Bullet(Bitmap image,Point point) 
        {
            this.image = image;
<<<<<<< HEAD
            this.pos = new Point(0, Game.Rnd.Next(0, Game.Height - image.Size.Height));            
            speed = 10;
=======
            this.size = image.Size;
            this.pos = point;            
            speed = 15;
>>>>>>> Future/edit
        }

        /// <summary>
        /// Реализация интерфейса ICollision
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Collision(ICollision obj) => obj.Rect.IntersectsWith(this.Rect);
        public Rectangle Rect => new Rectangle(pos, size);

        /// <summary>
        /// Перегрузка метода рисования
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, pos.X, pos.Y);
        }

        /// <summary>
        /// Обновление состояния объекта
        /// </summary>
<<<<<<< HEAD
        public override void ReCreation()
        {
            this.pos.X = 0;
            this.pos.Y = Game.Rnd.Next(0, Game.Height-image.Size.Height);
        }

        public override void Update()
        {
            pos.X = pos.X + speed;
            if (pos.X>Game.Width-image.Size.Width)
=======
        public override void Update<Bullet>(ref Bullet obj)
        {
            pos.X = pos.X + speed;
            if(pos.X>Game.Width)
>>>>>>> Future/edit
            {
                Destroy(ref obj);
            }
        }
    }
}
