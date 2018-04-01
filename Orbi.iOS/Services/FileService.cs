using System;
using System.IO;
using Orbi.Services;

namespace Orbi.iOS.Services
{
    public class FileService : IFileService
    {
        const string DATABASE_NAME = "data.sqlite";

        public FileService()
        {
        }

        public string ApplicationFolderPath => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public string DatabasePath => Path.Combine(ApplicationFolderPath, DATABASE_NAME);

        public void CopyFileFromAssets(string filename)
        {
            try
            {
                File.Copy(filename, Path.Combine(ApplicationFolderPath, filename), false);
            }
            catch (IOException)
            {

            }
        }

        public byte[] GetVideoFile(string filename)
        {
            var path = Path.Combine(ApplicationFolderPath, filename);
            return File.ReadAllBytes(path);
        }

        public void InitFiles()
        {
            CopyFileFromAssets(DATABASE_NAME);
            CopyFileFromAssets("1.jpg");
            CopyFileFromAssets("2.jpeg");
            CopyFileFromAssets("3.jpeg");
            CopyFileFromAssets("4.jpg");
            CopyFileFromAssets("5.jpg");
        }
    }
}
