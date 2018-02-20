using System;
using System.Windows.Forms;

namespace Asteroids
{
    /*Класс создает форму с предварительной проверкой 
     * ее ширины и высоты 
     */
    class MainForm:Form
    {
        const int defaultWidht=800;
        const int defaultHeight=600;
        /// <summary>
        /// Конструктор формы, задает размеры создаваемой формы
        /// </summary>
        /// <param name="widht">Ширина формы</param>
        /// <param name="height">Высота формы</param>
        public MainForm(int widht, int height) :base()
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

        //Проверка значения Widht и Height
        static void CheckParam(int widht, int height)
        {
            if (widht >= 1000 || widht < 0 || height >= 1000 || height < 0)
            {
                throw new ArgumentOutOfRangeException();                
            }
        }
    }
    
}
