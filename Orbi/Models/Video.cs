using System;
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
        [NotNull]
        public string Title { get; set; }
        public DateTime Created { get; set; }

        public Video()
        {
            GUID = Guid.NewGuid().ToString();
            Created = DateTime.UtcNow;
        }
    }
}
