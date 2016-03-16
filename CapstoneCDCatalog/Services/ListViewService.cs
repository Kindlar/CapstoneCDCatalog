using System.Collections.Generic;
using System.Linq;

namespace CapstoneCDCatalog.Services
{
    public class ListViewService
    {
        public SongService Service { get; set; } 

        public ListViewService()
        {
            Service = new SongService();
        }

        public AlbumSongView CreateViewItem(Song song)
        {
            var view = new AlbumSongView
            {
                SongTitle = song.SongTitle,
                SongRating = song.SongRating,
                AlbumTitle = Service.AlbumService.GetAlbumTitle(song.AlbumId),
                AlbumRating = Service.AlbumService.GetAlbumRating(song.AlbumId),
                TrackLengthSeconds = song.TrackLengthSeconds,
                AlbumYear = Service.AlbumService.GetAlbumYear(song.AlbumId),
                TrackNumber = song.TrackNumber,
                GenreName = Service.GenreService.GetGenre(song.GenreId)
             };
            
            return view;
        }

        public List<AlbumSongView> CreateViewItem(List<Song> songList)
        {
            return songList.Select(song => new AlbumSongView
            {
                SongTitle = song.SongTitle,
                SongRating = song.SongRating,
                AlbumTitle = Service.AlbumService.GetAlbumTitle(song.AlbumId),
                AlbumRating = Service.AlbumService.GetAlbumRating(song.AlbumId),
                TrackLengthSeconds = song.TrackLengthSeconds,
                AlbumYear = Service.AlbumService.GetAlbumYear(song.AlbumId),
                TrackNumber = song.TrackNumber,
                GenreName = Service.GenreService.GetGenre(song.GenreId)
            }).ToList();
        }
    }
}