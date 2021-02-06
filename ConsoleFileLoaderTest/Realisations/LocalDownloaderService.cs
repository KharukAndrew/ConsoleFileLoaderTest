using ConsoleFileLoaderTest.Interfaces;
using System.IO;

namespace ConsoleFileLoaderTest.Realisations
{
    public class LocalDownloaderService : IDownloaderService
    {
        public byte[] DownloadFileAsBytes(string filePath)
        {
            byte[] byteArray = new byte[0];

            using (FileStream fileStream = File.OpenRead(filePath))
            {
                byteArray = new byte[fileStream.Length];
                fileStream.Read(byteArray, 0, byteArray.Length);
            }
            return byteArray;
        }
    }
}
