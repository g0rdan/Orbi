using System;
using System.IO;
using Android.Content.Res;
using Orbi.Services;

namespace Orbi.Droid.Services
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
                if (!File.Exists(Path.Combine(ApplicationFolderPath, filename)))
                {
                    AssetManager assetManager = Android.App.Application.Context.Assets;
                    using (StreamReader sr = new StreamReader(assetManager.Open(filename)))
                    {
                        using (Stream file = File.Create(Path.Combine(ApplicationFolderPath, filename)))
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
