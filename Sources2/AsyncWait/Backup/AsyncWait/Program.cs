using System;
using System.IO;
using System.Threading;
using System.Text;

namespace AsyncWait
{
    class AsyncWaitClass
    {
        static void Main(string[] args)
        {
            //AsyncReadOneFile();

            //AsyncReadMultiplyFiles();

            FileStream fs = new FileStream( //поток
                @"../../Program.cs", // путь к файлу
                FileMode.Open, //режим открытия файла
                FileAccess.Read, //разрешение только на чтение
                FileShare.Read, // разрешение другим процессам читать файл
                1024, //размер буфера для чтения
                FileOptions.Asynchronous // указание на асинхронность
                );

            Byte[] data = new Byte[100]; //буфер для хранения прочитанных данных

            IAsyncResult ar = fs.BeginRead // начало асинхронной операции
                (data, //буфер куда записываются данные
                0, // смещение, откуда начинать читать
                data.Length, //клолисечтво байт для чтения
                null, // 
                null); //

            while (!ar.IsCompleted)// проверяем завершена ли асинхроннная операция
            {
                Console.WriteLine("Операция не завершена, ожидайте...");
                Thread.Sleep(10);
            }

            Int32 bytesRead = fs.EndRead(ar); // получение результата асинхронной операции (количество прочитанных байт)

            fs.Close(); // освобождаем ресурс

            Console.WriteLine("Количество считаных байт = {0}", bytesRead);
            Console.WriteLine(Encoding.UTF8.GetString(data).Remove(0, 1));
        }

        //Метод считывания из нескольких файлов
        private static void AsyncReadMultiplyFiles()
        {
            string[] files = {"../../Program.cs", //массив путей к файлам
                              "../../AsyncWait.csproj",
                              "../../Properties/AssemblyInfo.cs"};
            AsyncReader[] asrArr = new AsyncReader[3]; //массив объектов для асинхронного чтения
            for (int i = 0; i < asrArr.Length; ++i)
                asrArr[i] = new AsyncReader// создание массива потоков
                    (new FileStream(files[i], FileMode.Open, FileAccess.Read,
                     FileShare.Read, 1024, FileOptions.Asynchronous), 100);

            foreach (AsyncReader asr in asrArr)
                Console.WriteLine(asr.EndRead());
        }

        //Метод считывания из одного файла
        private static void AsyncReadOneFile()
        {
            FileStream fs = new FileStream(@"../../Program.cs", FileMode.Open, FileAccess.Read,
                                            FileShare.Read, 1024, FileOptions.Asynchronous);
            Byte[] data = new Byte[100];
            // Начало асинхронной операции чтения из файла FileStream. 
            IAsyncResult ar = fs.BeginRead(data, 0, data.Length, null, null);
            // Здесь выполняется другой код... 
            // Приостановка этого потока до завершения асинхронной операции 
            // и получения ее результата. 
            Int32 bytesRead = fs.EndRead(ar);
            // Других операций нет. Закрытие файла. 
            fs.Close();
            // Теперь можно обратиться к байтовому массиву и вывести результат операции. 
            Console.WriteLine("Количество прочитаных байт = {0}", bytesRead);

            Console.WriteLine(Encoding.UTF8.GetString(data).Remove(0, 1));
        }
    }

    class AsyncReader
    {
        FileStream stream; //файловый поток
        byte[] data; // буфер для данных
        IAsyncResult asRes; //объект для отслеживания асинхронной операции

        public AsyncReader(FileStream s, int size) //конструктор, инициализирующий асинхронное чтение
        {
            stream = s;
            data = new byte[size];
            asRes = s.BeginRead(data, 0, size, null, null);
        }

        public string EndRead() 
        {
            int countByte = stream.EndRead(asRes);// завершает чтение
            stream.Close(); // закрывает поток
            Array.Resize(ref data, countByte); 
            return string.Format("Файл: {0}\n{1}\n\n", stream.Name, Encoding.UTF8.GetString(data).Remove(0, 1));
                    //возвращает результат в виде строки
        }
    }
}
