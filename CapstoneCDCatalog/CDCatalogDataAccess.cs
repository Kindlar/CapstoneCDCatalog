using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CapstoneCDCatalog
{
    public class CDCatalogDataAccess
    {
        public List<Genre> GetGenreList()
        {
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                var genreList = db.Genres.ToList();
                return genreList;
            }
        }

        public List<Artist> GetArtistList()
        {
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                var artistList = db.Artists.ToList();
                return artistList;
            }
        }

        public List<Album> GetAlbumList()
        {
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                var albumList = db.Albums.ToList();
                return albumList;
            }
        }

        public List<Song> GetSongList()
        {
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                var songList = db.Songs.ToList();
                return songList;
            }
        }

        public void AddGenre(string genreToAdd)
        {
            if (DoesGenreExist(genreToAdd) == false)
            {
                using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
                {
                    Genre genre = new Genre();
                    genre.GenreName = genreToAdd;
                    db.Genres.Add(genre);
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("That genre already exisits.");
            }
        }

        public void AddArtist(string artistToAdd)
        {
            if (DoesArtistExist(artistToAdd) == false)
            {
                using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
                {
                    Artist artist = new Artist();
                    artist.ArtistName = artistToAdd;
                    db.Artists.Add(artist);
                    db.SaveChanges();
                }
            }           
        }
        public void AddAlbum(string albumTitle, string albumYear, string albumRating, string artistTitle)
        {
            int year, rating;
            int.TryParse(albumYear, out year);
            int.TryParse(albumRating, out rating);
            if (DoesAlbumExists(albumTitle, year))
            {
                var artist = GetArtistID(artistTitle);
                using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
                {
                    Album album = new Album();
                    album.AlbumTitle = albumTitle;
                    album.AlbumYear = year;
                    album.AlbumRating = rating;
                    album.ArtistId = artist.ArtistId;
                    db.Albums.Add(album);
                    db.SaveChanges();
                    //Need to verify if artist exists prior to attempting to add. 
                }
            }        
        }

        private bool DoesAlbumExists(string albumTitle, int albumYear)
        {
            var albumList = GetAlbumList();
            bool doesAlbumExist = false;
            foreach (var album in albumList)
            {
                if (albumTitle == album.AlbumTitle && albumYear == album.AlbumYear)
                    doesAlbumExist = true;
            }
            return doesAlbumExist;
        }

        private bool DoesArtistExist(string artistToAdd)
        {
            var artistList = GetArtistList();
            bool doesGenreExist = false;
            foreach (var artist in artistList)
            {
                if (artistToAdd == artist.ArtistName)
                {
                    doesGenreExist = true;
                }
            }
            return doesGenreExist;
        }

        private bool DoesGenreExist(string genreToAdd)
        {
            var genreList = GetGenreList();
            bool doesGenreExist = false;
            foreach (var genre in genreList)
            {
                if (genreToAdd == genre.GenreName)
                {
                    doesGenreExist = true;
                }
            }
            return doesGenreExist;
        }

        public void RemoveGenre(string genreToRemove)
        {
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                try
                {
                    if (DoesGenreExist(genreToRemove) == true)
                    {
                        Genre entry =
                            db.Genres.Single(id => id.GenreName == genreToRemove);
                        var value = db.Genres.Find(entry.GenreId);
                        db.Genres.Remove(value);
                        db.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Item does not exisit");
                    }
                }
                catch (InvalidOperationException mx)
                {
                    MessageBox.Show("There was a problem with your selection! Look: " + mx);
                }
                catch (Exception mx)
                {
                    MessageBox.Show("Something went wrong! Look: " + mx);
                }
            }
        }

        public List<Song> GetSongListByAlbum(string selectedItems)
        {
            var album = GetAlbumID(selectedItems);
      
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                var songList = db.Songs.Where(x => x.AlbumId.Value == album.AlbumId).Select(x => x);
                return songList.ToList();
            }        
        }

        private Album GetAlbumID(string selectedItem)
        {
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                Album album = db.Albums.Single(x => x.AlbumTitle == selectedItem);
                return album;
            }
        }

        public List<Song> GetSongListByGenre(string selectedItem)
        {
            var genre = GetGenreID(selectedItem);

            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                var songList = db.Songs.Where(x => x.GenreId == genre.GenreId).Select(x => x);
                return songList.ToList();
            }
        }

        private Genre GetGenreID(string selectedItem)
        {
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                Genre genre = db.Genres.Single(x => x.GenreName == selectedItem);
                return genre;
            }
        }

        public List<Song> GetSongListByArtist(string selectedItem)
        {
            var artist = GetArtistID(selectedItem);

            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                var songList = db.Songs.Where(x => x.ArtistId == artist.ArtistId).Select(x => x);
                return songList.ToList();
            }
        }

        private Artist GetArtistID(string selectedItem)
        {
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                Artist genre = db.Artists.Single(x => x.ArtistName == selectedItem);
                return genre;
            }
        }
    }
}