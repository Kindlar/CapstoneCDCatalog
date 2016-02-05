using System.Collections.Generic;
using System.Linq;

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
                Genre genre = new Genre {GenreName = genreToAdd};
                db.Genres.Add(genre);
                db.SaveChanges();
             }           
        }

        private bool DoesGenreExist(string genreToAdd)
        {
            bool result = false; 
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                var genre = db.Genres.FirstOrDefault(x => x.GenreName == genreToAdd);
                if(genre != null) result = true;
            }
            return result;
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