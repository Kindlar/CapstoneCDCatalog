using System.Collections.Generic;
using System.Linq;

namespace CapstoneCDCatalog.Services
{
    public class SongService
    {
        public SongService()
        {
            AlbumService = new AlbumService(this);
            ArtistService = new ArtistService();
        }

        public AlbumService AlbumService { get; }
        public ArtistService ArtistService { get; }
        public GenreService GenreService { get; } = new GenreService();

        public List<Song> GetSongList()
        {
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                var songList = db.Songs.ToList();
                return songList;
            }
        }

        public List<Song> GetSongListByAlbum(string selectedItems)
        {
            var album = AlbumService.GetAlbumID(selectedItems);
      
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                var songList = db.Songs.Where(x => x.AlbumId.Value == album.AlbumId).Select(x => x);
                return songList.ToList();
            }        
        }

        public List<Song> GetSongListByArtist(string selectedItem)
        {
            var artist = ArtistService.GetArtistID(selectedItem);

            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                var songList = db.Songs.Where(x => x.ArtistId == artist.ArtistId).Select(x => x);
                return songList.ToList();
            }
        }
    }
}