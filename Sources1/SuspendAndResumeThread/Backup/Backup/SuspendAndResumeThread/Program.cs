using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace SuspendAndResumeThread
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadStart ts = new ThreadStart(Method);
            Thread t = new Thread(ts);
            t.Start(); // Запуск потока.

            Console.WriteLine($"Имя потока: {t.Name}");   
            Console.WriteLine($"Запущен ли поток: {t.IsAlive}");
            Console.WriteLine($"Id потока: {t.ManagedThreadId}");
            Console.WriteLine($"Приоритет потока: {t.Priority}");
            Console.WriteLine($"Статус потока: {t.ThreadState}");

            /*
            реализовать возможность множественной приостановки и продолжения
            */
            while (t.IsAlive) //проверяем закрыт/открыт ли поток
            {
                Console.WriteLine("Нажмите любую клавишу для остановки");
                Console.ReadKey();
                //Thread.Sleep(Timeout.Infinite);
                t.Suspend(); // Приостановка потока.
                Console.WriteLine("Поток остановлен!");
                Console.WriteLine("Нажмите любую клавишу для возобновления");
                Console.ReadKey();
                t.Resume(); // Возобновление работы.
            }
        }

        static void Method()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(100);
            }
            Console.WriteLine("Программа завершена.");
        }
    }
}
