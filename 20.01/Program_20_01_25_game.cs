namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Приготовьтесь!");
            Thread.Sleep(2000); 
            while (Console.KeyAvailable) Console.ReadKey(true);//Отсекаем "Фальстарт!" 
            Console.WriteLine("Жми!");
            DateTime time1 = DateTime.Now;
            Console.WriteLine(time1);
            //Console.ReadKey(false);
            Console.ReadKey(true);
            DateTime time2 = DateTime.Now;
            Console.WriteLine(time2);
            TimeSpan timeRez = time2 - time1;
            Console.WriteLine($"Получилось за {timeRez.TotalMilliseconds} мс");        }
    }
}