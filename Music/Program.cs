using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Music.Classes;

namespace Music
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString="Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog = MusicDatabase;Integrated Security=true;MultipleActiveResultSets = true";
            using (var connection = new SqlConnection(connectionString))
            {
                var artists = connection.Query<Artist>("select * from Artist");
                foreach (var artist in artists)
                {
                    Console.WriteLine(artist.Name);
                }
                var albums = connection.Query<Album>("select * from Album");
                foreach (var album in albums)
                {
                    Console.WriteLine(album.Name);
                }

                var songs = connection.Query<Song>("select * from Song");
                foreach (var song in songs)
                {
                    Console.WriteLine(song.Name+" "+song.Length );
                }

                var albumSongs = connection.Query<AlbumSong>("select * from AlbumSong");
                foreach (var albumSong in albumSongs)
                {
                    Console.WriteLine(albumSong.AlbumId + " " + albumSong.SongId);
                }

                //dodamo svim glazbenicima njihove albume
                foreach (var artist in artists)
                {
                    foreach (var album in albums)
                    {
                            if(artist.ArtistId==album.ArtistId)
                                artist.Albums.Add(album);
                    }
                }

                //provjera
                //foreach (var artist in artists)
                //{
                //    Console.WriteLine($"{artist.Name} {artist.ArtistId} {artist.Albums.ElementAt(0).Name} {artist.Albums.ElementAt(0).ArtistId}");
                //}

                //dodamo svim albumima njihove pjesme
                foreach (var album in albums)
                    foreach (var song in songs)
                        foreach (var albumSong in albumSongs)
                            if (album.AlbumId == albumSong.AlbumId && song.SongId == albumSong.SongId)
                            {
                                album.Songs.Add(song);
                                song.Albums.Add(album);
                            }

                //provjera
                //foreach (var album in albums)
                //{
                //    Console.WriteLine($"{album.Name} {album.AlbumId} {album.Songs.ElementAt(0).Name} {album.Songs.ElementAt(0).Albums.ElementAt(0).AlbumId}");
                //}




            }
        }
    }
}
