namespace ConsoleFileLoaderTest.Interfaces
{
    public interface IDownloaderService
    {
        byte[] DownloadFileAsBytes(string filePath);
    }
}
