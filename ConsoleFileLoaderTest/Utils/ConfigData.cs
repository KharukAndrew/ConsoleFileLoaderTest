namespace ConsoleFileLoaderTest.Utils
{
    public static class ConfigData
    {
        public static string YandexToken { get; } = "введите токен";
        public static string LocalDirectory { get; } = @"введите директорию";
        public static string yandexPathForLoader { get; } = "https://cloud-api.yandex.net/v1/disk/resources/upload?path=";
    }
}
