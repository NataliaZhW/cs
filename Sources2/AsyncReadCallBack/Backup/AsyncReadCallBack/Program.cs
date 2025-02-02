﻿using System;
using System.IO;
using System.Threading;
using System.Text;

namespace AsyncReadCallBack
{
    class AsyncReadCallBackClass
    {
        private static Byte[] staticData = new Byte[100];

        static void Main(string[] args)
        {
            AsyncReadOneFileCallBack();
            //AsyncReadOneFileCallBackAnonimus();
            //AsyncReadMultiplyFilesAnonimus();


        }

        private static void AsyncReadOneFileCallBack()
        {
            Console.WriteLine("Основной поток ID = {0}",
               Thread.CurrentThread.ManagedThreadId);

            FileStream fs = new FileStream(@"../../Program.cs", FileMode.Open,
                                           FileAccess.Read, FileShare.Read, 1024,
                                           FileOptions.Asynchronous);

            fs.BeginRead(staticData, 0, staticData.Length, 
                ReadIsComplete, //метод обратного вызова (CallBack), будет вызван после завершения чтения
                fs);// объект который будет передан в этот (CallBack) метод 
            Console.ReadLine();
        }

        private static void ReadIsComplete(IAsyncResult ar)
        {
            Console.WriteLine("Чтение в потоке {0} закончено",
               Thread.CurrentThread.ManagedThreadId); 

            FileStream fs = (FileStream)ar.AsyncState; 

            Int32 bytesRead = fs.EndRead(ar);

            fs.Close();

            Console.WriteLine("Количество считаных байт = {0}", bytesRead);
            Console.WriteLine(Encoding.UTF8.GetString(staticData).Remove(0, 1)); //декодируем и выводим сколько получилось
        }

        private static void AsyncReadOneFileCallBackAnonimus()
        {
            Byte[] data = new Byte[100]; //локальный буфер вместо статического

            Console.WriteLine("Основной поток ID = {0}",
               Thread.CurrentThread.ManagedThreadId);

            FileStream fs = new FileStream(@"../../Program.cs", FileMode.Open,
                                           FileAccess.Read, FileShare.Read, 1024,
                                           FileOptions.Asynchronous);

            fs.BeginRead(data, 0, data.Length, delegate(IAsyncResult ar) // анонимный метод обратного вызова через делегат
            {
                Console.WriteLine("Чтение в потоке {0} закончено",
                Thread.CurrentThread.ManagedThreadId);

                Int32 bytesRead = fs.EndRead(ar);

                fs.Close();

                Console.WriteLine("Количество считаных байт = {0}", bytesRead);
                Console.WriteLine(Encoding.UTF8.GetString(data).Remove(0, 1));

                Console.ReadLine();
            }, null);
            Console.ReadLine();
        }

        private static void AsyncReadMultiplyFilesAnonimus()
        {
            string[] files = {"../../Program.cs", 
                              "../../AsyncReadCallBack.csproj", 
                              "../../Properties/AssemblyInfo.cs"};

            for (int i = 0; i < files.Length; ++i)
                new AsyncCallBackReader(new FileStream(files[i], FileMode.Open, FileAccess.Read,
                                        FileShare.Read, 1024, FileOptions.Asynchronous), 100,
                                          delegate(Byte[] data)
                                          {
                                              // Process the data.
                                              Console.WriteLine("Количество прочитаных байт = {0}", data.Length);
                                              Console.WriteLine(Encoding.UTF8.GetString(data).Remove(0, 1) + "\n\n");
                                          });
            Console.ReadLine();
        }
    }

    public delegate void AsyncBytesReadDel(Byte[] streamData);

    class AsyncCallBackReader
    {
        FileStream stream;
        byte[] data;
        IAsyncResult asRes;
        AsyncBytesReadDel callbackMethod; // делегат для обратного вызова после завершения чтения

        public AsyncCallBackReader(FileStream s, int size, AsyncBytesReadDel meth) //инкапсуляция логики асинхронного чтения
        {
            stream = s;
            data = new byte[size];
            callbackMethod = meth;
            asRes = s.BeginRead(data, 0, size, ReadIsComplete, null);
        }

        public void ReadIsComplete(IAsyncResult ar)
        {
            int countByte = stream.EndRead(asRes);
            stream.Close();
            Array.Resize(ref data, countByte);
            callbackMethod(data);
        }
    }
}
