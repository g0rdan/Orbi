using System;
using System.Collections.Generic;
using System.Linq;
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
            }
            catch (SQLiteException)
            {

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

            }
        }

        public void AddVideo(Video video, Album album)
        {
            try
            {
                _connection?.Insert(video);
                _connection?.Insert(new AlbumVideo(album.GUID, video.GUID));
            }
            catch (SQLiteException)
            {

            }
        }

        public void DeleteAlbum(Album album)
        {
            try
            {
                _connection?.Delete(album);
            }
            catch (SQLiteException)
            {

            }
        }

        public void DeleteVideo(Video video)
        {
            try
            {
                _connection?.Delete(video);
            }
            catch (SQLiteException)
            {

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
                var query = "SELECT v, cp.id_category " +
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
    }
}
