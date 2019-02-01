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
            System.Collections.Generic.IEnumerable<Artist> artists;
            System.Collections.Generic.IEnumerable<Album> albums;
            System.Collections.Generic.IEnumerable<Song> songs;
            System.Collections.Generic.IEnumerable<AlbumSong> albumSongs;
            var connectionString="Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog = MusicDatabase;Integrated Security=true;MultipleActiveResultSets = true";
            using (var connection = new SqlConnection(connectionString))
            {
                artists = connection.Query<Artist>("select * from Artist");
                albums = connection.Query<Album>("select * from Album");
                songs = connection.Query<Song>("select * from Song");
                albumSongs = connection.Query<AlbumSong>("select * from AlbumSong");
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
                

                //dodamo svim albumima njihove pjesme i pjesmama albume
                foreach (var album in albums)
                    foreach (var song in songs)
                        foreach (var albumSong in albumSongs)
                            if (album.AlbumId == albumSong.AlbumId && song.SongId == albumSong.SongId)
                            {
                                album.Songs.Add(song);
                                song.Albums.Add(album);
                            }

                //sve glazbenike po imenu
                var artistsByName = artists.OrderBy(artist => artist.Name);
                Console.WriteLine("Glazbenici poredani po imenu");
                foreach (var artist in artistsByName)
                {
                    Console.WriteLine($"Name:{artist.Name} Id:{artist.ArtistId}");
                }

                //svi glazbenici određene nacionalnosti
                var americanArtists = artists.Where(artist => artist.Nationality == "American");
                Console.WriteLine("\nSvi glazbenici kojima je nacionalnost \"American\"");
                foreach (var artist in americanArtists)
                {
                    Console.WriteLine($"Name:{artist.Name} Nationality:{artist.Nationality}");
                }

                var englishArtists = artists.Where(artist => artist.Nationality == "English");
                Console.WriteLine("Svi glazbenici kojima je nacionalnost \"English\"");
                foreach (var artist in englishArtists)
                {
                    Console.WriteLine($"Name:{artist.Name} Nationality:{artist.Nationality}");
                }

                //Svi albumi grupirani po godini izdavanja, kraj imena albuma piše tko je autor (glazbenik)
                var albumsByYear = albums.OrderBy(album => album.YearPublished);
                Console.WriteLine("\nAlbumi grupirani po godini izdavanja");
                foreach (var album in albumsByYear)
                {
                    var name = artists.FirstOrDefault(artist => artist.ArtistId == album.ArtistId).Name;
                    Console.WriteLine($"Name:{album.Name}, Year:{album.YearPublished}, Artist:{name}");
                }

                //Svi albumi koji sadrze u imenu zadani tekst
                var text = "hi";
                Console.WriteLine("\nAlbumi koji u imenu sadrze "+text);
                var albumsContainingText = albums.Where(album => album.Name.Contains(text));
                foreach (var album in albumsContainingText)
                {
                    Console.WriteLine($"Name:{album.Name} Id:{album.AlbumId}");
                }

                text = "How to";
                Console.WriteLine("Albumi koji u imenu sadrze " + text);
                albumsContainingText = albums.Where(album => album.Name.Contains(text));
                foreach (var album in albumsContainingText)
                {
                    Console.WriteLine($"Name:{album.Name} Id:{album.AlbumId}");
                }

                //Svi albumi skupa sa ukupnim trajanjem - ukupno trajanje je zbroj trajanja svih pjesama na albumu
                Console.WriteLine("\nAlbumi skupa sa ukupnim trajanjem");
                var albumsWithLength = albums.Select(album => album.Name+ " "+
                    Math.Round(album.Songs.Select(song => song.Length).Sum(),2));
                foreach (var albumNameAndLength in albumsWithLength)
                {
                    Console.WriteLine(albumNameAndLength);
                }

                //Console.WriteLine("Provjera za Zaba");
                //var zaba = albums.First(album => album.Name == "Zaba");
                //var songsFromZaba = songs.Where(song => song.Albums.Contains(zaba));
                //foreach (var song in songsFromZaba)
                //{
                //    Console.WriteLine(song.Name + " " + Math.Round(song.Length, 2));
                //}

                //Svi albumi na kojima se pojavljuje zadana pjesma
                var chosenSong = songs.FirstOrDefault(song => song.Name == "Sorry");
                Console.WriteLine("\nSvi albumi na kojima se pojavljuje "+chosenSong.Name);
                var albumsWithSong = albums.Where(album => album.Songs.Contains(chosenSong));
                foreach (var album in albumsWithSong)
                {
                    Console.WriteLine($"Name:{album.Name} Id:{album.AlbumId} Year:{album.YearPublished}");
                }

                //Sve pjesme zadanog glazbenika, koje su na albumima izdanim iza određene godine
                
                var chosenArtist = artists.FirstOrDefault(artist => artist.Name == "Glass Animals");
                var year = 2007;
                Console.WriteLine($"\nPjesme glazbenika {chosenArtist.Name} na albumima izdanim nakon {year}");
                var albumsFromSelectedArtistPublishedAfterSelectedYear = albums.Where(album =>
                    (album.YearPublished >= year && album.ArtistId == chosenArtist.ArtistId));
                foreach (var album in albumsFromSelectedArtistPublishedAfterSelectedYear)
                {
                    var songsOnAlbumsAfterSelectedYear = songs.Where(song =>
                        album.Songs.Contains(song));
                    foreach (var song in songsOnAlbumsAfterSelectedYear)
                    {
                        Console.WriteLine($"Pjesma {song.Name} je na albumu {album.Name} izdanom {album.YearPublished} ");
                    }
                }

                year = 2015;
                Console.WriteLine($"\nPjesme glazbenika {chosenArtist.Name} na albumima izdanim nakon {year}");
                albumsFromSelectedArtistPublishedAfterSelectedYear = albums.Where(album =>
                    (album.YearPublished >= year && album.ArtistId == chosenArtist.ArtistId));
                foreach (var album in albumsFromSelectedArtistPublishedAfterSelectedYear)
                {
                    var songsOnAlbumsAfterSelectedYear = songs.Where(song =>
                        album.Songs.Contains(song));
                    foreach (var song in songsOnAlbumsAfterSelectedYear)
                    {
                        Console.WriteLine($"Pjesma {song.Name} je na albumu {album.Name} izdanom {album.YearPublished} ");
                    }
                }
                
        }
    }
}
