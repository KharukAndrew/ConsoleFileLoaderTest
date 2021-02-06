namespace ConsoleFileLoaderTest.Interfaces
{
    public interface IInformService
    {
        (int, int) AddInfo(string text);
        (int, int) UpdateInfo((int, int) cursor, string text);
    }
}
