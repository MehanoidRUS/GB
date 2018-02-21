using System;
using System.Windows.Forms;
using System.Drawing;

namespace Asteroids
{
    /*Класс отвечающий за создание формы и отрисовку
     */
    class MainForm:Form
    {
        //Значение ширины и высоты формы по-умолчанию
        const int defaultWidht=800;
        const int defaultHeight=600;

        private  BufferedGraphicsContext gameContext;
        public  BufferedGraphics Buffer;
        /// <summary>
        /// Конструктор формы по-умолчанию
        /// </summary>
        public MainForm():base()
        {
            //Width = defaultWidht;
            //Height = defaultHeight;
            Graphics graphics;
            graphics = CreateGraphics();
            gameContext = BufferedGraphicsManager.Current;
            graphics = CreateGraphics();
            Buffer = gameContext.Allocate(graphics, new Rectangle(0, 0, Width, Height));
        }


        /// <summary>
        /// Конструктор формы, задающий размеры создаваемой формы
        /// </summary>
        /// <param name="widht">Ширина формы</param>
        /// <param name="height">Высота формы</param>
        public MainForm(int widht, int height):this()
        {
            try
            {
                CheckParam(widht, height);
                Width = widht;
                Height = height;

            }
            catch (ArgumentOutOfRangeException)
            {
                Width = defaultWidht;
                Height = defaultHeight;
            }

        }

        /// <summary>
        /// Метод проверки получаемых значений ширины и высоты формы
        /// </summary>
        /// <param name="widht">Ширина формы</param>
        /// <param name="height">Высота формы</param>
        static void CheckParam(int widht, int height)
        {
            if (widht >= 1000 || widht < 0 || height >= 1000 || height < 0)
            {
                throw new ArgumentOutOfRangeException();                
            }
        }
    }
    
}
