using System;
using System.Windows.Forms;

namespace Asteroids
{
    /*Класс создает форму с предварительной проверкой 
     * ее ширины и высоты 
     */
    static class MainForm
    {
        const int defaultWidht=800;
        const int defaultHeight=600;

        //Создает форму 
        static public Form CreateForm(int widht, int height)
        {
            Form form = new Form();
            try
            {
                CheckParam(widht,height);
                form.Width = widht;
                form.Height = height;

            }
            catch (ArgumentOutOfRangeException)
            {
                form.Width = defaultWidht;
                form.Height = defaultHeight;
            }


            return form;
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
