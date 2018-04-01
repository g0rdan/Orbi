using System;
using System.Collections.Generic;
using SQLite;

namespace Orbi.Models
{
    /// <summary>
    /// Represent an album item
    /// </summary>
    [Table("Album")]
    public class Album
    {
        [PrimaryKey, NotNull]
        public string GUID { get; set; }
        [NotNull]
        public string Title { get; set; }
        public DateTime Created { get; set; }
            
        public Album()
        {
            GUID = Guid.NewGuid().ToString();
            Created = DateTime.UtcNow;
        }
	}
}
