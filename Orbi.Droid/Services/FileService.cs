using System;
using System.IO;
using Android.Content.Res;
using Orbi.Services;

namespace Orbi.Droid.Services
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
                if (!File.Exists(REAL_DATABASE_NAME))
                {
                    AssetManager assetManager = Android.App.Application.Context.Assets;
                    using (StreamReader sr = new StreamReader(assetManager.Open(EMPTY_DATABASE_NAME)))
                    {
                        using (Stream file = File.Create(DatabasePath))
                        {
                            CopyStream(sr.BaseStream, file);
                        }
                    }
                }
            }
            catch (IOException)
            {

            }
        }

        void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }
    }
}
