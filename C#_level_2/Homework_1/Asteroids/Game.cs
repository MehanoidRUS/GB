using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Media;

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
        //Основной таймер
        static Timer timer = new Timer { Interval = 100 };
        //Интервал времени, через которой запуститься бонус в мс.
        static int BonusTimeMin = 10000;
        static int BonusTimeMax = 20000;
        //Таймер запуска бонуса(интервал в диапазоне 10..20сек)
        static Timer timerBonus=new Timer { Interval = Game.Rnd.Next(BonusTimeMin,BonusTimeMax)};

        static Star[] stars;
        //static Star[] staticStars;
        static Asteroid[] ListAsteroid;
        static Bullet[] bullets;
        static uint score = 0;
        static Bitmap imageAsteroid;
        static Bitmap imageBullet;
        static Ship ship;
        static HeartForLife heart;

        /// <summary>
        /// Описываем методы для журналирования
        /// </summary>
        /// <param name="Message"></param>
        delegate void Logging(string Message);

        /// <summary>
        /// Событие запускающие логирование
        /// </summary>
        static event Logging Log;

        /// <summary>
        /// Метод журналирования в консоль
        /// </summary>
        /// <param name="msg">Текст сообщения</param>
        static void JournalConsole(string msg)
        {
            Console.WriteLine($"{msg}");
        }

        /// <summary>
        /// Метод журналирования в файл
        /// </summary>
        /// <param name="msg">Текст сообщения</param>
        static void JournalFile(string msg)
        {
            string FilePath = $@"{Directory.GetCurrentDirectory()}\Log";
            if (!File.Exists(FilePath))
            {
                DirectoryInfo info;
                info=Directory.CreateDirectory($@"{Directory.GetCurrentDirectory()}\Log");
            }
            using (StreamWriter inFile=new StreamWriter(@"Log\gamelog.txt",true))
            {
                inFile.WriteLine(msg);
            }
        }

        static Game()
        {

        }

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="form">Главная форма приложения</param>
        public static void Init(MainForm form)
        {
            timer.Start();
            timer.Tick += Timer_Tick;
            timerBonus.Tick += TimerBonus_Tick;
            Log += JournalConsole;
            Log += JournalFile;
            Width = form.Width;
            Height = form.Height;
            Buffer = form.Buffer;            
            form.KeyDown += Form_KeyDown;
            Ship.MessageDie += GameOver;

            Load();
        }

        /// <summary>
        /// Обработка нажатия клавиш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                for (int i = 0; i < bullets.Length; i++)
                {
                    if (bullets[i] == null)
                    {
                        bullets[i] = new Bullet(imageBullet, ship.ShipPosition + ship.Size);
                        break;
                    }
                }
            }
            if (e.KeyCode == Keys.Up) ship.Up();
            if (e.KeyCode == Keys.Down) ship.Down();      
        }

        public static void Load()
        {
            ship = new Ship();
            bool isMove = false;
            stars = new Star[amountAllStars];
            bullets = new Bullet[5];
            imageAsteroid = new Bitmap(@"Image\asteroid64x64.png");
            imageBullet = new Bitmap(@"Image\bullet_ship.png");
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
            foreach (var bullet in bullets)
            {
                bullet?.Draw();
            }
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

            for (int i = 0; i < ListAsteroid.Length; i++)
            {

                if (ListAsteroid[i] == null)
                {
                    ListAsteroid[i] = new Asteroid(imageAsteroid);
                }
                ListAsteroid[i]?.Update(ref ListAsteroid[i]);
                if (BulletHits(ref ListAsteroid[i]))
                {
                    continue;
                }
                if (ListAsteroid[i]!=null && ship.Collision(ListAsteroid[i]))
                {
                    Log("Корабль подбит");
                    timerBonus.Start();
                    ListAsteroid[i] = null;
                    ship.Damage();
                }

            }
            for (int i = 0; i < bullets.Length; ++i)
            {
                if (bullets[i] != null)
                {
                    bullets[i].Update(ref bullets[i]);
                }
            }
            SystemSounds.Asterisk.Play();
            ship.Update(ref ship);
            if (ship.Live <= 0) ship?.Die();
            if (heart != null)
            {
                if (heart.Collision(ship))
                {
                    Log("Колличество жизней увеличено");
                    ship.Heal();
                    heart = null;
                }
                heart?.Update(ref heart);
            }
            GC.Collect();
        }

        /// <summary>
        /// Проверка на попадание в астеройд
        /// </summary>
        /// <param name="asteroid">объект класса Asteroid</param>
        /// <returns></returns>
        static bool BulletHits(ref Asteroid asteroid)
        {
            for (int i=0;i<bullets.Length;++i)
            {
                if (bullets[i] != null && asteroid != null)
                {
                    if (bullets[i].Collision(asteroid))
                    {
                        Log("Астеройд уничтожен");
                        SystemSounds.Hand.Play();
                        score++;
                        asteroid = null;
                        bullets[i] = null;
                        return true;
                    }
                }
            }            
            return false;
        }

        /// <summary>
        /// Метод обработки события timer.Tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Tick(object sender,EventArgs e)
        {
            Draw();
            Update();
                      
        }
        /// <summary>
        /// Метод обработки события timerBonus.Tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void TimerBonus_Tick(object sender,EventArgs e)
        {
            StartBonus();
        }

        /// <summary>
        /// Метод обработки при пройгреше
        /// </summary>
        public static void GameOver()
        {           
            string text = "Game Over";
            timer.Stop();
            Buffer.Graphics.DrawString(text, new Font(FontFamily.GenericSansSerif,60,FontStyle.Underline), Brushes.WhiteSmoke,400,200);
            Buffer.Render();
            Log($"{text} Колличество сбитых астеройдов = {score}");
        }
        
        /// <summary>
        /// Метод запуска бонуса, по таймеру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void StartBonus()
        {
            timerBonus.Stop();
            heart = new HeartForLife();
            Log($"Аптечка создана.");
            timerBonus.Interval = Game.Rnd.Next(BonusTimeMin, BonusTimeMax);
        }
    }
}
