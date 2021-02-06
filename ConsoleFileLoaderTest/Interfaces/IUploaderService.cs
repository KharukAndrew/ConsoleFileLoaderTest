namespace ConsoleFileLoaderTest.Interfaces
{
    public interface IUploaderService
    {
        bool UploadFile(string filePath, string fileName, string fileExtensions, byte[] file);
    }
}
