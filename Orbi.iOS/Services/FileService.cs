using System;
using System.IO;
using Orbi.Services;

namespace Orbi.iOS.Services
{
    public class FileService : IFileService
    {
        const string EMPTY_DATABASE_NAME = "empty.sqlite";
        const string REAL_DATABASE_NAME = "data.sqlite";

        public FileService()
        {
        }

        public string ApplicationFolderPath => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public string DatabasePath => Path.Combine(ApplicationFolderPath, REAL_DATABASE_NAME);

        public void CopyEmptyDatabase()
        {
            try
            {
                File.Copy(EMPTY_DATABASE_NAME, DatabasePath, false);
            }
            catch (IOException)
            {

            }
        }
    }
}
