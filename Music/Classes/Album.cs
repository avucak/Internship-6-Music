using System;
using System.Collections.Generic;
using System.Text;

namespace Music.Classes
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string Name { get; set; }
        public int YearPublished { get; set; }
        public int ArtistId { get; set; }
    }
}
