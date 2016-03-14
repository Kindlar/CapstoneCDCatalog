namespace CapstoneCDCatalog.Services
{
    public class PlayListViewService
    {
        public AlbumSongView View { get; }
        public SongService Service { get; set; } 

        public PlayListViewService()
        {
            View = new AlbumSongView();
            Service = new SongService();
        }

        public AlbumSongView CreateViewItem(Song song)
        {
            var view = new AlbumSongView
            {
                SongTitle = song.SongTitle,
                SongRating = song.SongRating,
                AlbumTitle = song.Album.AlbumTitle,
                AlbumRating = song.Album.AlbumRating,
                TrackLengthSeconds = song.TrackLengthSeconds,
                AlbumYear = song.Album.AlbumYear,
                TrackNumber = song.TrackNumber,
                GenreName = Service.GenreService.GetGenre(song.GenreId)
             };
            
            return view;
        }

    }
}