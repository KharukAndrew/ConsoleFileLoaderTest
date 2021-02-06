using ConsoleFileLoaderTest.Interfaces;
using ConsoleFileLoaderTest.Models;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;

namespace ConsoleFileLoaderTest.Realisations
{
    public class YandexUploaderService : IUploaderService
    {
        private readonly string _token;

        public YandexUploaderService()
        { }
        public YandexUploaderService(string token)
        {
            _token = token;
        }

        public bool UploadFile(string yandexPathForLoader, string fileName, string fileExtensions, byte[] file)
        {
            string fullYandexPathWithName = GetFullPathWithName(yandexPathForLoader, fileName, fileExtensions);
            string pathForValidate = GetPathForValidate(fullYandexPathWithName);

            int i = 1;
            while (IsFileWithThatPath(pathForValidate))
            {
                fullYandexPathWithName = GetFullPathWithName(yandexPathForLoader, fileName, fileExtensions, $"({i})");
                pathForValidate = GetPathForValidate(fullYandexPathWithName);
                i++;
            }

            string url = GetYandexUrl(fullYandexPathWithName);
            return UploadFileAsBytes(url, file);
        }

        private string GetYandexUrl(string fullYandexPathWithName)
        {
            var request = WebRequest.Create(fullYandexPathWithName);
            request.Headers["Authorization"] = "OAuth " + _token;
            request.Method = "GET";

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                var url = JsonConvert.DeserializeObject<YandexResponse>(reader.ReadToEnd());
                return url.Href;
            }
        }

        private bool UploadFileAsBytes(string url, byte[] byteFile)
        {
            var request = WebRequest.Create(url);
            request.Method = "PUT";
            request.ContentType = "application/binary";

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(byteFile, 0, byteFile.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.Created)
                return true;
            else
                return false;
        }

        private bool IsFileWithThatPath(string fullPathForValidate)
        {
            var request = WebRequest.Create(fullPathForValidate);
            request.Headers["Authorization"] = "OAuth " + _token;
            request.Method = "GET";

            try
            {
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                if (response.StatusCode == HttpStatusCode.OK)
                    return true;
                else
                    return false;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        private string GetFullPathWithName(string path, string name, string extension, string number = "")
        {
            return $"{path}{name}{number}{extension}";
        }

        private string GetPathForValidate(string fullPath)
        {
            return fullPath.Replace("/upload", "");
        }
    }
}
