using System;
using System.Collections.Generic;
using SQLite;

namespace Orbi.Models
{
    /// <summary>
    /// Represent a video item
    /// </summary>
    [Table("Video")]
    public class Video
    {
        [PrimaryKey, NotNull]
        public string GUID { get; set; }
        [NotNull]
        public string FileName { get; set; }
        /// README: this property is temporary representing video data (video file)
        /// It means I won't use videos as files for now. It'll store here, in database
        public byte[] Data { get; set; }
        public DateTime Created { get; set; }

        public Video()
        {
            GUID = Guid.NewGuid().ToString();
            Created = DateTime.UtcNow;
        }
    }
}
