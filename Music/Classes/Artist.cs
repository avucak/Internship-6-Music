using System;
using System.Collections.Generic;
using System.Text;

namespace Music.Classes
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public List<Album> Albums { get; set; }

        public Artist()
        {
            Albums = new List<Album>();
        }
    }
}
