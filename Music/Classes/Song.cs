using System;
using System.Collections.Generic;
using System.Text;

namespace Music.Classes
{
    public class Song
    {
        public int SongId { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public List<Album> Albums { get; set; }

        public Song()
        {
            Albums = new List<Album>();
        }
    }
}
