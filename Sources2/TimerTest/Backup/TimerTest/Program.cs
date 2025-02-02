﻿using System;
using System.Threading;

namespace TimerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer t = new Timer
                (TimerMethod, //метод который будет вызываться (обратного вызова, то что вызовется в конце)
                null, //объект, параметр состояния, (здесь - ничего )
                0, // время ожидания до первого вызова в мс
                1000); //периодичность вызовов в мс

            Console.WriteLine("Основной поток {0} продолжается...", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(5000);
            t.Dispose();
        }

        static void TimerMethod(Object obj)
        {
            Console.WriteLine("Поток {0} : {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);
        }
    }
}
