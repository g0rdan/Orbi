using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orbi.Models;
using SQLite;

namespace Orbi.Services
{
    public class DatabaseService : IDatabaseService
    {
        SQLiteConnection _connection;
        readonly IFileService _fileService;

        public DatabaseService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public void InitConnection()
        {
            if (_connection == null && !string.IsNullOrWhiteSpace(_fileService.DatabasePath))
            {
                _connection = new SQLiteConnection(_fileService.DatabasePath);
            }
        }

        public void InitTables()
        {
            try
            {
                _connection?.CreateTable<Album>(CreateFlags.None);
                _connection?.CreateTable<Video>(CreateFlags.None);
                _connection?.CreateTable<AlbumVideo>(CreateFlags.None);
                FillWithData();
            }
            catch (SQLiteException)
            {
                //handling situation
            }
        }

        public void AddAlbum(Album album)
        {
            try
            {
                _connection?.Insert(album);
            }
            catch (SQLiteException)
            {
                //handling situation
            }
        }

        public void AddVideo(Video video)
        {
            try
            {
                _connection?.Insert(video);
            }
            catch (SQLiteException)
            {
                //handling situation
            }
        }

        [Obsolete("Obsolete method", true)]
        public void AddVideo(Video video, Album album)
        {
            try
            {
                _connection?.Insert(video);
                _connection?.Insert(new AlbumVideo(album.GUID, video.GUID));
            }
            catch (SQLiteException)
            {
                //handling situation
            }
        }

        public void AddVideo(string videoGuid, string audioGuid)
        {
            try
            {
                _connection?.Insert(new AlbumVideo(audioGuid, videoGuid));
            }
            catch (SQLiteException)
            {
                //handling situation
            }
        }

        public void DeleteAlbum(Album album)
        {
            try
            {
                _connection?.Delete(album);
                var relaitedItems = _connection.Query<AlbumVideo>($"SELECT * FROM AlbumVideo WHERE Album_GUID = '{album.GUID}'");
                if (relaitedItems != null && relaitedItems.Any())
                {
                    foreach (var item in relaitedItems)
                    {
                        _connection.Delete(item);
                    }
                }
            }
            catch (SQLiteException)
            {
                //handling situation
            }
        }

        public void DeleteAlbum(string guid)
        {
            try
            {
                var albumModel = _connection.Get<Album>(guid);
                if (albumModel != null)
                    DeleteAlbum(albumModel);
            }
            catch (SQLiteException)
            {
                //handling situation
            }
        }

        public void DeleteVideo(Video video)
        {
            try
            {
                _connection?.Delete(video);
                var query = $"DELETE FROM {nameof(AlbumVideo)} WHERE {nameof(AlbumVideo.Video_GUID)} = '{video.GUID}'";
                _connection.Execute(query);
            }
            catch (SQLiteException)
            {
                //handling situation
            }
        }

        public void DeleteVideo(Video video, Album album)
        {
            try
            {
                _connection?.Delete(new AlbumVideo(album.GUID, video.GUID));
            }
            catch (SQLiteException)
            {
                //handling situation
            }
        }

        public void DeleteVideo(string videoGuid, string albumGuid)
        {
            try
            {
                var query = $"SELECT * FROM {nameof(AlbumVideo)} WHERE {nameof(AlbumVideo.Album_GUID)} = '{albumGuid}' AND {nameof(AlbumVideo.Video_GUID)} = '{videoGuid}'";
                var id = _connection?.Query<AlbumVideo>(query).FirstOrDefault()?.Index ?? 0;
                _connection?.Delete<AlbumVideo>(id);
            }
            catch (SQLiteException)
            {
                //handling situation
            }
        }

        public Album GetAlbum(string gUID)
        {
            try
            {
                return _connection.Get<Album>(gUID);
            }
            catch (SQLiteException)
            {
                return new Album();
            }
        }

        public List<Album> GetAlbums()
        {
            try
            {
                return _connection?.Table<Album>().ToList();
            }
            catch (SQLiteException)
            {
                return new List<Album>();
            }
        }

        public Video GetVideo(string guid)
        {
            try
            {
                return _connection?.Get<Video>(guid);
            }
            catch (SQLiteException)
            {
                return new Video();
            }
        }

        public List<Video> GetVideos()
        {
            try
            {
                return _connection?.Table<Video>().ToList();
            }
            catch (SQLiteException)
            {
                return new List<Video>();
            }
        }

        public List<Video> GetVideos(Album album)
        {
            try
            {
                var query = "SELECT v.GUID, v.FileName, v.Name " +
                            "FROM Video as v " +
                            "INNER JOIN AlbumVideo as av " +
                                "ON v.GUID = av.Video_GUID " +
                            "INNER JOIN Album as a " +
                                "ON av.Album_GUID = a.GUID " +
                            $"WHERE a.GUID = '{album.GUID}'";

                return _connection?.Query<Video>(query);
            }
            catch (SQLiteException)
            {
                return new List<Video>();
            }
        }

        // TODO: Add cancelation tokens
        public async Task<List<Album>> GetAlbumsAsync()
        {
            return await Task.Factory.StartNew(GetAlbums).ConfigureAwait(false);
        }

        public async Task<List<Video>> GetVideosAsync()
        {
            return await Task.Factory.StartNew(GetVideos).ConfigureAwait(false);
        }

        public async Task<List<Video>> GetVideosAsync(Album album)
        {
            return await Task.Factory.StartNew(() => { return GetVideos(album); } ).ConfigureAwait(false);
        }

        /// <summary>
        /// Helps with filling the data at first launch
        /// </summary>
        void FillWithData()
        {
            var videos = GetVideos();
            if (videos == null || !videos.Any())
            {
                _connection?.InsertAll(new List<Video> {
                        new Video { FileName = "1.jpg", Name = "Melvy" },
                        new Video { FileName = "2.jpeg", Name = "Marvin" },
                        new Video { FileName = "3.jpeg", Name = "Boris" },
                        new Video { FileName = "4.jpg", Name = "Snowflake" },
                        new Video { FileName = "5.jpg", Name = "Crew" },
                    });
            }
        }
    }
}
