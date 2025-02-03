using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        // Импорт функции MessageBox из user32.dll с переименованием
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBoxW(IntPtr hWnd, string text, string caption, uint type);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        // Константы для сообщений
        const uint WM_SETTEXT = 0x000C; // Сообщение для изменения текста заголовка окна
        const uint WM_CLOSE = 0x0010;   // Сообщение для закрытия окна

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Задание 1: Отображение MessageBox с "Hello, World!"
            MessageBoxW(IntPtr.Zero, "Hello, World!", "Сообщение", 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Задание 2: Игра "Угадай число"
            int min = 0;
            int max = 100;
            bool flag = false;

            while (!flag)
            {
                int number = (min + max) / 2;

                int result = MessageBoxW(IntPtr.Zero, $"Ваше число {number}?", "Угадай число", 3); // 3 - Да, Нет, Отмена

                if (result == 6) // Да
                {
                    MessageBoxW(IntPtr.Zero, $"Ваше число {number}!", "Ура!", 0); flag = true;
                }
                else if (result == 7) // Нет
                {
                    result = MessageBoxW(IntPtr.Zero, $"Ваше число больше, чем {number}?", "Угадай число", 3);
                    if (result == 6) min = number + 1;
                    else if (result == 7) max = number - 1;
                    else break;
                }
                else // Отмена
                {
                    break;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Задание 3: Закрытие окна Блокнота
            IntPtr hWnd = FindWindow("Notepad", null);
            if (hWnd != IntPtr.Zero)
            {
                SendMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                MessageBox.Show("Один Блокнот закрыт");
                //MessageBoxW(IntPtr.Zero, $"Ваше число {number}!", "Ура!", 0);
            }
            else
            {
                MessageBox.Show("Блокнот не найден");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Задание 4: Вывести в заголовке Блокнота текущее время
            // Найти Блокнот или запустить
            IntPtr hWnd = FindWindow("Notepad", null);
            if (hWnd == IntPtr.Zero)
            {
                MessageBox.Show("Блокнот не найден");
                //Process.Start(@"notepad.exe");                
            }
            else
            {
                // Запуск таймера для обновления заголовка
                new Thread(() =>
                {
                    while (hWnd != IntPtr.Zero)
                    {
                        string currentTime = DateTime.Now.ToString("HH:mm:ss");
                        SendMessage(hWnd, WM_SETTEXT, IntPtr.Zero, Marshal.StringToHGlobalUni(currentTime));
                        Thread.Sleep(1000); // Пауза 1 секунда
                    }
                }).Start();
            }
        }
    }
}