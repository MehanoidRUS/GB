using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;


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
        //Через сколько запускать аптечку, завязана на таймер
        static int launchFrequencyBonus;
        static Timer timer = new Timer { Interval = 100 };
        static Star[] stars;
        //static Star[] staticStars;
        static Asteroid[] ListAsteroid;
        static Bullet bullet;
        static uint score = 0;
        static Bitmap imageAsteroid;
        static Bitmap imageBullet;
        static Ship ship;
        static HeartForLife heart;

        delegate void Logging(string Message);

        static event Logging Log;
        static void JournalConsole(string msg)
        {
            Console.WriteLine($"{msg}");
        }

        static void JournalFile(string msg)
        {
            Console.WriteLine($@"{Directory.GetCurrentDirectory()}");
            string FilePath = $@"{Directory.GetCurrentDirectory()}\Log";
            if (!File.Exists(FilePath))
            {
                DirectoryInfo info;
                info=Directory.CreateDirectory($@"{Directory.GetCurrentDirectory()}\Log");
                Console.WriteLine($@"{info.FullName}");
            }
            using (StreamWriter inFile=new StreamWriter(@"Log\gamelog.txt",true))
            {
                inFile.WriteLine(msg);
            }
        }

        static Game()
        {

        }

        public static void Init(MainForm form)
        {
            timer.Start();
            timer.Tick += Timer_Tick;
            timer.Tick += StartBonus;
            Log += JournalConsole;
            Log += JournalFile;
            Width = form.Width;
            Height = form.Height;
            Buffer = form.Buffer;            
            form.KeyDown += Form_KeyDown;
            Ship.MessageDie += GameOver;

            Load();
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
           if(e.KeyCode==Keys.ControlKey) bullet = new Bullet(imageBullet,ship.ShipPosition+ship.SizeObject);
            if (e.KeyCode == Keys.Up) ship.Up();
            if (e.KeyCode == Keys.Down) ship.Down();      
        }

        public static void Load()
        {
            ship = new Ship();
            bool isMove = false;
            stars = new Star[amountAllStars];
            imageAsteroid = new Bitmap(@"Image\asteroid64x64.png");
            imageBullet = new Bitmap(@"Image\bullet_ship.png");
           // launchFrequencyBonus = Game.Rnd.Next(100, 1000);
            launchFrequencyBonus = 50;
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
            Log($"{DateTime.Now} Start Game");
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (Star star in stars)
            {
                star?.Draw();
            }
            foreach (var ast in ListAsteroid)
            {
                ast?.Draw();
            }
            ship?.Draw();
            bullet?.Draw();
            heart?.Draw();


            Buffer.Graphics.DrawString($"Сбито астеройдов: {score}", new Font("Arial", 16), Brushes.WhiteSmoke, new Point(10, 10));
            Buffer.Graphics.DrawString($"Осталось жизней: {ship.Live}", new Font("Arial", 16), Brushes.WhiteSmoke, new Point(10, 30));
            Buffer.Render();
        }

        public static void Update()
        {
            for (int i = 0; i < stars.Length; i++)
            {
                if (stars[i]==null)
                {
                    stars[i]= new Star(new Size(Rnd.Next(0, 5), Rnd.Next(0, 5)), true);
                }
                else
                {
                    stars[i].Update(ref stars[i]);
                }
                if (stars[i].Position.X < 0)
                {
                    stars[i] = null;
                }
            }
            bullet?.Update(ref bullet);
            for (int i = 0; i < ListAsteroid.Length; i++)
            {

                if (ListAsteroid[i] == null)
                {
                    Log($"ListAsteroid[{i}]");
                    ListAsteroid[i] = new Asteroid(imageAsteroid);
                    Log($"ListAsteroid[{i}].X={ListAsteroid[i].Position.X}");
                }
                ListAsteroid[i]?.Update(ref ListAsteroid[i]);
                if (bullet!=null && ListAsteroid[i]!=null && bullet.Collision(ListAsteroid[i]))
                {
                    Log("Астеройд уничтожен");
                    System.Media.SystemSounds.Hand.Play();
                    Console.WriteLine("BOOM!!!");
                    score++;
                    ListAsteroid[i] = null;
                    bullet = null;
                    continue;
                }
                if (ListAsteroid[i]!=null && ship.Collision(ListAsteroid[i]))
                {
                    Log("Корабль подбит");
                    ListAsteroid[i] = null;
                    ship.Damage();
                }

            }

            System.Media.SystemSounds.Asterisk.Play();
            if (ship.Live == 0) ship?.Die();
            if (heart != null)
            {
                heart.Update(ref heart);
                if (heart.Collision(ship))
                {
                    Log("Колличество жизней увеличено");
                    ship.Heal();
                    heart = null;
                }
            }

            GC.Collect();
        }

        private static void Timer_Tick(object sender,EventArgs e)
        {
            Update();
            Draw();
            
        }

        public static void GameOver()
        {           
            string text = "Game Over";            
            Buffer.Graphics.DrawString(text, new Font("Arial", 16), Brushes.WhiteSmoke, new Point(Width/2 - text.Length, Height / 2));
            Buffer.Render();
            timer.Stop();
            Log($"{text} Колличество сбитых астеройдов = {score}");
        }
        
        private static void StartBonus(object sender, EventArgs e)
        {

            if (launchFrequencyBonus <= 0 && heart == null)
            {
                //Console.WriteLine("Create Heart");
                //heart = new HeartForLife();
                launchFrequencyBonus = 1;
                
            }
                if (heart == null)
            {
                //Console.WriteLine($"{launchFrequencyBonus}");
                launchFrequencyBonus--;
                //Console.WriteLine($"Position x={ListAsteroid[1].Position.X}");
            }
        }
    }
}
