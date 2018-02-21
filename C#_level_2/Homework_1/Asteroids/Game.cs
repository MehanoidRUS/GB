using System;
using System.Windows.Forms;
using System.Drawing;


namespace Asteroids
{
    static class Game
    {
        public static Random Rnd = new Random();
        public static int Width { get; private set; }
        public static int Height { get; private set; }
        public static BufferedGraphics Buffer;
        //Константа общее колличество звезд
        const int amountAllStars = 80;
        //Константа, колличество не подвижных звезд
        const int amountStaticStars = 50;
        static Star[] stars;
        //static Star[] staticStars;
        static Asteroid[] ListAsteroid;
        static Bullet bullet;
        static uint score = 0;
        static Bitmap imageAsteroid;
        static Bitmap imageBullet;
        static Ship ship = new Ship(new Point(400, 300));
        static Game()
        {

        }

        public static void Init(MainForm form)
        {
            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
            Width = form.Width;
            Height = form.Height;
            Buffer = form.Buffer;
            
            form.KeyDown += Form_KeyDown;
            Load();
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
           // if(e.KeyCode==Keys.ControlKey) bullet = new Bullet(imageBullet,ship.ShipPosition+ship.SizeObject);
            
        }

        public static void Load()
        {
            
            bool isMove = false;
            stars = new Star[amountAllStars];
            imageAsteroid = new Bitmap(@"Image\asteroid64x64.png");
            imageBullet = new Bitmap(@"Image\bullet_ship.png");
            bullet = new Bullet(imageBullet, new Point(400,0));
            ListAsteroid = new Asteroid[10];
            

            //Цикл создает подвижные и неподвижные звезды
            for (int i = 0; i < amountAllStars; i++)
            {
                if (i > amountStaticStars)
                {
                    isMove = true; 
                }
                stars[i] = new Star(new Size(Rnd.Next(0, 5), Rnd.Next(0, 5)), isMove); 
            }
            //Цикл создает астеройды
            for (int i = 0; i < ListAsteroid.Length; i++)
            {
                ListAsteroid[i] = new Asteroid(imageAsteroid);
            }
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            //foreach (Star star in staticStars)
            //{
            //    star.Draw();
            //}
            foreach (Star star in stars)
            {
                star.Draw();
            }
            foreach (var ast in ListAsteroid)
            {
                ast.Draw();
            }
            ship.Draw();
            bullet.Draw();
            Buffer.Graphics.DrawString($"Сбито астеройдов: {score}", new Font("Arial", 16), Brushes.WhiteSmoke, new Point(10, 10));
            Buffer.Render();
        }

        public static void Update()
        {
            foreach (Star obj in stars)
            {
                obj.Update();
            }
            foreach (Asteroid ast in ListAsteroid)
            {
                ast.Update();
                Collision(ast, bullet);
            }
            bullet.Update();
            GC.Collect();
        }

        private static void Timer_Tick(object sender,EventArgs e)
        {
            Draw();
            Update();
        }
        /// <summary>
        /// Метод проверки на столкновение
        /// </summary>
        /// <param name="ast">Объект класса Asteroid</param>
        /// <param name="bullet">Объект класса Bullet</param>
        private static void Collision(Asteroid ast,Bullet bullet)
        {
            if (ast.Collision(bullet))
            {
                bullet.ReCreation();
                ast.ReCreation();
                score++;
            }
        }
    }
}
