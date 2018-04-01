using System;
namespace Orbi.Services
{
    public interface IFileService
    {
        /// <summary>
        /// Full path to the app folder
        /// </summary>
        string ApplicationFolderPath { get; }
        /// <summary>
        /// Full path to the database
        /// </summary>
        string DatabasePath { get; }
        /// <summary>
        /// Coping a file from asset folders to work at first
        /// application launch
        /// </summary>
        void CopyFileFromAssets(string filename);
        /// <summary>
        /// Inits the files.
        /// </summary>
        void InitFiles();
        /// <summary>
        /// Gets video files from home folder as byte array
        /// </summary>
        byte[] GetVideoFile(string filename);
    }
}
