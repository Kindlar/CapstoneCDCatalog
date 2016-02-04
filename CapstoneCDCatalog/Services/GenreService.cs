using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CapstoneCDCatalog.Services
{
    public class GenreService
    {
        public List<Genre> GetGenreList()
        {
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                var genreList = db.Genres.ToList();
                return genreList;
            }
        }

        public void AddGenre(string genreToAdd)
        {
                using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
                {
                    Genre genre = new Genre();
                    genre.GenreName = genreToAdd;
                    db.Genres.Add(genre);
                    db.SaveChanges();
                }           
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

        public List<Song> GetSongListByGenre(string selectedItem)
        {
            var genreID = GetGenreID(selectedItem);

            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                var songList = db.Songs.Where(x => x.GenreId == genreID).Select(x => x);
                return songList.ToList();
            }
        }
         
        public int GetGenreID(string selectedItem)
        {
            if (!DoesGenreExist(selectedItem)) AddGenre(selectedItem);
            else
            {
                using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
                {
                    Genre genre = db.Genres.Single(x => x.GenreName == selectedItem);
                    return genre.GenreId;
                }
            }
        return GetGenreID(selectedItem);
        }
    }
}