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
            this.size = image.Size;
            this.pos = point;            
            speed = 10;
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
        public override void Update<Bullet>(ref Bullet obj)
        {
            pos.X = pos.X + speed;
            if(pos.X>Game.Width)
            {
                Destroy(ref obj);
            }
        }
    }
}
