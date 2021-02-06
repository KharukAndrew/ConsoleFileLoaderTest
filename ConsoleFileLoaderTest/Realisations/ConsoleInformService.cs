using ConsoleFileLoaderTest.Interfaces;
using System;

namespace ConsoleFileLoaderTest.Realisations
{
    public class ConsoleInformService : IInformService
    {
        private readonly object consoleLock = new object();
       
        public (int, int) AddInfo(string text)
        {
            int cursorLeft = 0;
            int cursorTop = 0;           
            lock (consoleLock)
            {
                Console.Write(text);               
                cursorLeft = Console.CursorLeft;
                cursorTop = Console.CursorTop;
                Console.WriteLine();
            }           
            return (cursorLeft, cursorTop);
        }

        public (int, int) UpdateInfo((int, int) cursor, string text)
        {
            int cursorLeft = cursor.Item1;
            int cursorTop = cursor.Item2;
            lock (consoleLock)
            {
                Console.SetCursorPosition(cursorLeft - 11, cursorTop);
                Console.Write(text);
                cursorLeft = Console.CursorLeft;
                cursorTop = Console.CursorTop;
            }
            return (cursorLeft, cursorTop);
        }
    }
}
