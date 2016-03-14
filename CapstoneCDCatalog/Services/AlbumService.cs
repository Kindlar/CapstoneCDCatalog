using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows.Documents;

namespace CapstoneCDCatalog.Services
{
    public class AlbumService
    {
        private readonly SongService songService;

        public AlbumService(SongService songService)
        {
            this.songService = songService;
        }

        public AlbumService()
        {
            
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

        public Album GetAlbum(string albumTitle)
        {
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                var album = db.Albums.FirstOrDefault(x => x.AlbumTitle.ToLower() == albumTitle.ToLower());
                if (album != null) return album;
            }

            return null;
        }
        public Album GetAlbum(int albumID)
        {
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                var album = db.Albums.FirstOrDefault(x => x.AlbumId == albumID);
                if (album != null) return album;
            }
            return null;
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

        public List<Album> DoesAlbumExists(string titleToSearchFor)
        {
            var album = new List<Album>();
            {
                using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
                {
                    album = new List<Album> { db.Albums.FirstOrDefault(x => x.AlbumTitle.ToLower() == titleToSearchFor.ToLower()) };
                    if (album != null)
                        return album;
                }
            }
            return null;
        }

        public List<Album> GetAlbumsByArtist(string artistToSearchFor)
        {
            var album = new List<Album>();
            {
                using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
                {
                    album =  db.Albums.Where(x => x.Artist.ArtistName == artistToSearchFor.ToLower()).ToList();
                    if (album != null)
                        return album;
                }
            }
            return null;
        }

        public void UpdateAlbumRating(string oldAlbum, int rating)
        {
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                    var newAlbum = db.Albums.FirstOrDefault(x => x.AlbumTitle == oldAlbum);
                    newAlbum.AlbumRating = rating;
                    db.SaveChanges();
            }
        }
    }
}