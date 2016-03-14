using System.Collections.Generic;
using System.Windows;
using CapstoneCDCatalog.Services;

namespace CapstoneCDCatalog
{
    public partial class FindByTitleWindow : Window
    {
        public SongService SongAccess { get; set; }

        public FindByTitleWindow()
        {
            InitializeComponent();
            SongAccess = new SongService();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if (titleToSearchForTextBox.Text != string.Empty)
            {
                string titleToSearchFor = titleToSearchForTextBox.Text;
                
                messageTextBlock.Text = SearchForSong(titleToSearchFor) ? "The song has been found!" : "The song was not found";
                messageTextBlock.Text = SearchForAlbum(titleToSearchFor) ? "The album has been found!" : "The album or song does not exist.";
            }
        }

        private bool SearchForAlbum(string titleToSearchFor)
        {
            List<Album> album = new List<Album>();
            album = SongAccess.AlbumService.DoesAlbumExists(titleToSearchFor);
            if (album[0] != null)
            {
                albumDataGrid.ItemsSource = album;
                return true;
            }
            return false;
        }

        private bool SearchForSong(string titleToSearchFor)
        {
            List<Song> song = new List<Song>();
            song = SongAccess.DoesSongExist(titleToSearchFor);
            if (song[0] != null)
            {
                songOrCdDataGrid.ItemsSource = song;
                return true;
            }
            return false;
        }

        private void songOrCdDataGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "SongID" || e.PropertyName == "GenreId" || e.PropertyName == "AlbumId" || e.PropertyName == "ArtistId")
            {
                e.Cancel = true;
            }
        }
    }
}
