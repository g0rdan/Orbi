﻿using System;
using System.Collections.Generic;
using Orbi.Models;

namespace Orbi.Services
{
    /// <summary>
    /// Represents service which works with database where stores all
    /// information and relations about Albums and Videos
    /// </summary>
    public interface IDatabaseService
    {
        /// <summary>
        /// Init database connection
        /// </summary>
        void InitConnection();
        /// <summary>
        /// Init database and other stuff if it happens at first time
        /// </summary>
        void InitTables();
        /// <summary>
        /// Get albums from database
        /// </summary>
        /// <returns>The albums.</returns>
        List<Album> GetAlbums();
        /// <summary>
        /// Get all videos from database
        /// </summary>
        List<Video> GetVideos();
        /// <summary>
        /// Gets the videos from database
        /// </summary>
        /// <param name="album">Get specific album videos</param>
        List<Video> GetVideos(Album album);
        /// <summary>
        /// Adding album to the database
        /// </summary>
        void AddAlbum(Album album);
        /// <summary>
        /// Deleting album from database.
        /// It won't delete videos in that album
        /// </summary>
        void DeleteAlbum(Album album);
        /// <summary>
        /// Adding video without specific album
        /// </summary>
        void AddVideo(Video video);
        /// <summary>
        /// Adding video to a specific album
        /// </summary>
        void AddVideo(Video video, Album album);
        /// <summary>
        /// Deleting video from database completely
        /// </summary>
        void DeleteVideo(Video video);
        /// <summary>
        /// Deleting video from a specific album in database.
        /// </summary>
        void DeleteVideo(Video video, Album album);
    }
}