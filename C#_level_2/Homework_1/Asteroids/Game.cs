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
        static Asteroid[] ListAsteroid;
        static Bullet bullet;
        static uint score = 0;

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
            ListAsteroid = new Asteroid[10];
            bullet = new Bullet();
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new Star(new Size(Rnd.Next(0, 5), Rnd.Next(0, 5)));
            }
            for (int i = 0; i < ListAsteroid.Length; i++)
            {
                ListAsteroid[i] = new Asteroid();
            }
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
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
        //Провеярет на столкновение
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
