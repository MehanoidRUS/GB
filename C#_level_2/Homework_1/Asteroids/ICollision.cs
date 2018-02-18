using System;
using System.Drawing;

namespace Asteroids
{
    //Интерфейс столкновения объектов
    interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle Rect { get; }
    }
}
