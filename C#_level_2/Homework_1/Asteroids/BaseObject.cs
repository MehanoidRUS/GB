﻿using System;
using System.Drawing;


namespace Asteroids
{
    abstract class BaseObject
    {
        protected Point pos;
        protected Point dir;
        protected int speed = 1;
        protected Size size;

        protected BaseObject(Size size):this()
        {            
            this.size = size;
        }

        protected BaseObject()
        {
            this.pos = new Point(Game.Width + 10, Game.Rnd.Next(0, Game.Height));
            this.dir = new Point(Game.Rnd.Next(2,10), Game.Rnd.Next(1, 20));
        }

        public abstract void Draw();

        public abstract void Update();
        
        //Метод задает новые параметры объекта
        public abstract void ReCreation();

    }
}
