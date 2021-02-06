using ConsoleFileLoaderTest.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleFileLoaderTest.Realisations
{
    public class ParallelLoadManager : ILoadManager
    {
        private readonly IDownloaderService _downloadService;
        private readonly IUploaderService _uploadService;
        private readonly IInformService _informService;

        public ParallelLoadManager(IDownloaderService downloadService,
                                IUploaderService uploadService,
                                IInformService informService)
        {
            _downloadService = downloadService;
            _uploadService = uploadService;
            _informService = informService;
        }

        public bool LoadFiles(string from, string to)
        {
            if (!Directory.Exists(from))
            {
                _informService.AddInfo("Указанная директория отсутствует");
                return false;
            }
            string[] files = Directory.GetFiles(from);

            try
            {
                Parallel.ForEach(files, currentFile => DoLoad(currentFile, to));
                return true;
            }
            catch (Exception ex)
            {
                _informService.AddInfo(ex.Message);
                return false;
            }           
        }

        private void DoLoad(string outputFilePath, string to)
        {
            string fileName = Path.GetFileNameWithoutExtension(outputFilePath);
            string fileExtensions = Path.GetExtension(outputFilePath);

            byte[] fileBytes = _downloadService.DownloadFileAsBytes(outputFilePath);
           
            (int, int) cursor = _informService.AddInfo($"{fileName}{fileExtensions} - загружается");

            if (fileBytes.Length > 0)
            {
                bool result = _uploadService.UploadFile(to, fileName, fileExtensions, fileBytes);

                if (result)
                    _informService.UpdateInfo(cursor, $"загрузился ");
            }
        }
    }
}
