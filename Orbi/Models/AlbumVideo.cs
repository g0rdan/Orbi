using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Orbi.Models
{
    /// <summary>
    /// The bound table for many-to-many relations
    /// </summary>
    [Table("AlbumVideo")]
    public class AlbumVideo
    {
        [ForeignKey(typeof(Album))]
        public string Album_GUID { get; set; }
        [ForeignKey(typeof(Video))]
        public string Video_GUID { get; set; }

        public AlbumVideo()
        {
        }

        public AlbumVideo(string album_guid, string video_guid)
        {
            Album_GUID = album_guid;
            Video_GUID = video_guid;
        }
    }
}
