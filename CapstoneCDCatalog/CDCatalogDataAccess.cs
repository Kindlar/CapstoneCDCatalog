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
            if (DoesGenreExisit(genreToAdd) == false)
            {
                using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
                {
                    DoesGenreExisit(genreToAdd);
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

        private bool DoesGenreExisit(string genreToAdd)
        {
            var genreList = GetGenreList();
            bool doesGenreExisit = false;
            foreach (var genre in genreList)
            {
                if (genreToAdd == genre.GenreName)
                {
                    doesGenreExisit = true;
                }
            }
            return doesGenreExisit;
        }

        public void RemoveGenre(string genreToRemove)
        {
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                try
                {
                    if (DoesGenreExisit(genreToRemove) == true)
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