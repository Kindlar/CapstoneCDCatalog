using System.Windows.Controls;

namespace CapstoneCDCatalog
{
    public class CellFormating
    {
        public static void SupressIdValues(DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "SongID" ||
                e.PropertyName == "GenreId" ||
                e.PropertyName == "AlbumId" ||
                e.PropertyName == "ArtistId" ||
                e.PropertyName == "SongArtistID")
            {
                e.Cancel = true;
            }
        }

        public static void SuppressAlbumData(DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Artist" || e.PropertyName == "Songs")
                e.Cancel = true;
        }

        public static void SpaceOutNames(DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "SongTitle":
                    e.Column.Header = "Song Title";
                    break;
                case "SongRating":
                    e.Column.Header = "Song Rating";
                    break;
                case "TrackLengthSeconds":
                    e.Column.Header = "Track Length in Seconds";
                    break;
                case "AlbumRating":
                    e.Column.Header = "Album Rating";
                    break;
                case "AlbumYear":
                    e.Column.Header = "Album Year";
                    break;
                case "TrackNumber":
                    e.Column.Header = "Track Number";
                    break;
                case "AlbumTitle":
                    e.Column.Header = "Album";
                    break;
                case "GenreName":
                    e.Column.Header = "Genre";
                    break;
            }
        }
    }
}