using System.Collections.Generic;
using System.Linq;

namespace CapstoneCDCatalog.Services
{
    public class ArtistService
    {
        public List<Artist> GetArtistList()
        {
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                var artistList = db.Artists.ToList();
                return artistList;
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

        private bool DoesArtistExist(string artistToAdd)
        {
            var artistList = GetArtistList();
            bool doesAlbumExist = false;
            foreach (var artist in artistList)
            {
                if (artistToAdd == artist.ArtistName)
                {
                    doesAlbumExist = true;
                }
            }
            return doesAlbumExist;
        }

        public Artist GetArtistID(string selectedItem)
        {
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                Artist genre = db.Artists.Single(x => x.ArtistName == selectedItem);
                return genre;
            }
        }
    }
}