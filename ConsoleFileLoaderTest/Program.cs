using ConsoleFileLoaderTest.Interfaces;
using ConsoleFileLoaderTest.Realisations;
using ConsoleFileLoaderTest.Utils;
using System;

namespace ConsoleFileLoaderTest
{
    class Program
    {
        static string localDirectory = string.Empty;
        static string yandexPath = string.Empty;
        static string token = ConfigData.YandexToken;

        static void Main(string[] args)
        {
            GetAndValidInputdata(args);

            Console.WriteLine("Нажмите на любую клавишу для начала загрузки");
            Console.ReadKey();

            ILoadManager manager = new ParallelLoadManager(new LocalDownloaderService(),
                                                            new YandexUploaderService(token),
                                                            new ConsoleInformService());
            bool result = manager.LoadFiles(localDirectory, yandexPath);

            if (result)
            {
                Console.WriteLine("\nФайлы загружены");
            }              
            else
                Console.WriteLine("\nЧто-то пошло не так");

            Console.ReadKey();
        }

        static void GetAndValidInputdata(string[] args)
        {
            if (args.Length == 2)
            {
                localDirectory = args[0];
                yandexPath = args[1];
            }
            else
            {
                localDirectory = ConfigData.LocalDirectory;
                yandexPath = ConfigData.yandexPathForLoader;
            }
        }
    }
}
