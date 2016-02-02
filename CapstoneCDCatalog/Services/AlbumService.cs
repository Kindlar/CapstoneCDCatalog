using System.Collections.Generic;
using System.Linq;

namespace CapstoneCDCatalog.Services
{
    public class AlbumService
    {
        private readonly SongService songService;

        public AlbumService(SongService songService)
        {
            this.songService = songService;
        }

        public List<Album> GetAlbumList()
        {
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                var albumList = db.Albums.ToList();
                return albumList;
            }
        }

        public void AddAlbum(string albumTitle, string albumYear, string albumRating, string artistTitle)
        {
            int year, rating;
            int.TryParse(albumYear, out year);
            int.TryParse(albumRating, out rating);
            if (DoesAlbumExists(albumTitle, year))
            {
                var artist = songService.ArtistService.GetArtistID(artistTitle);
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

        public Album GetAlbumID(string selectedItem)
        {
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                Album album = db.Albums.Single(x => x.AlbumTitle == selectedItem);
                return album;
            }
        }
    }
}