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
            {
                using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
                {
                    Artist artist = new Artist {ArtistName = artistToAdd};
                    db.Artists.Add(artist);
                    db.SaveChanges();
                }
            }           
        }

        private bool DoesArtistExist(string artistToAdd)
        {
            bool doesArtistExist = false;
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                var artist = db.Artists.FirstOrDefault(x => x.ArtistName == artistToAdd);
                if (artist != null) doesArtistExist = true;
            }
            return doesArtistExist;
        }

        public int GetArtistId(string selectedItem)
        {
            if (!DoesArtistExist(selectedItem)) AddArtist(selectedItem);
            else
            {
                using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
                {
                    Artist artist = db.Artists.Single(x => x.ArtistName == selectedItem);
                    return artist.ArtistId;
                }
            }
            return GetArtistId(selectedItem);
        }
    }
}