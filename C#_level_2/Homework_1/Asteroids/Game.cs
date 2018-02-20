using System;
using System.Windows.Forms;
using System.Drawing;


namespace Asteroids
{
    static class Game
    {
        private static BufferedGraphicsContext gameContext;
        public static Random Rnd = new Random();
        public static BufferedGraphics Buffer;
        public static int Width { get; set; }
        public static int Height { get; set; }
        static Star[] stars;
        static Star[] staticStars;
        static Asteroid[] ListAsteroid;
        static Bullet bullet;
        static uint score = 0;
        static Bitmap imageAsteroid;
        static Bitmap imageBullet;

        static Game()
        {

        }

        public static void Init(Form form)
        {
            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
            Graphics graphics;
            Width = form.Width;
            Height = form.Height;
            gameContext = BufferedGraphicsManager.Current;
            graphics = form.CreateGraphics();
            Buffer = gameContext.Allocate(graphics, new Rectangle(0, 0, Width, Height));
            Load();
        }

        public static void Load()
        {
            stars = new Star[30];
            staticStars = new Star[50];
            imageAsteroid = new Bitmap(@"Image\asteroid64x64.png");
            imageBullet = new Bitmap(@"Image\bullet.png");
            ListAsteroid = new Asteroid[10];
            bullet = new Bullet(imageBullet);
            
            for (int i = 0; i < staticStars.Length; i++)
            {
                staticStars[i] = new Star(new Size(Rnd.Next(1, 6), Rnd.Next(1, 6)), false);
            }
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new Star(new Size(Rnd.Next(0, 5), Rnd.Next(0, 5)),true);
            }
            for (int i = 0; i < ListAsteroid.Length; i++)
            {
                ListAsteroid[i] = new Asteroid(imageAsteroid);
            }
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (Star star in staticStars)
            {
                star.Draw();
            }
            foreach (Star obj in stars)
            {
                obj.Draw();
            }
            foreach (var ast in ListAsteroid)
            {
                ast.Draw();
            }
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
        /// Метод проверки столкновения
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
