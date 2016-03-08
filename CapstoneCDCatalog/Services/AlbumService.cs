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

        public void AddAlbum(string albumTitle, int albumYear, int albumRating, string artistTitle)
        {
                var artistID = songService.ArtistService.GetArtistID(artistTitle);
                using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
                {
                    Album album = new Album
                    {
                        AlbumTitle = albumTitle,
                        AlbumYear = albumYear,
                        AlbumRating = albumRating,
                        ArtistId = artistID
                    };
                    db.Albums.Add(album);
                    db.SaveChanges();
                 }
        }

        public bool DoesAlbumExists(string albumTitle, int albumYear)
        {
            bool doesAlbumExist = false;
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                var album = db.Albums.FirstOrDefault(x => x.AlbumTitle.ToLower() == albumTitle.ToLower());
                if (album != null) doesAlbumExist = true;   
            }

            return doesAlbumExist;
        }

        public int GetAlbumID(string selectedItem)
        {
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                Album album = db.Albums.Single(x => x.AlbumTitle == selectedItem);
                return album.AlbumId;
            }
        }

        internal int GetAlbumID(string album, int albumYear, int albumRating, string artist)
        {
            if(!DoesAlbumExists(album, albumYear)) AddAlbum(album, albumYear, albumRating, artist);
            return GetAlbumID(album);
        }
    }
}