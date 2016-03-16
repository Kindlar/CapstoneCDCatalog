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
            if (titleToSearchForTextBox.Text == string.Empty) return;
            var titleToSearchFor = titleToSearchForTextBox.Text;
                
            messageTextBlock.Text = SearchForSong(titleToSearchFor) ? "The song has been found!" : "The song was not found";
            messageTextBlock.Text = SearchForAlbum(titleToSearchFor) ? "The album has been found!" : "The album or song does not exist.";
        }

        private bool SearchForAlbum(string titleToSearchFor)
        {
            var album = SongAccess.AlbumService.DoesAlbumExists(titleToSearchFor);
            if (album[0] == null) return false;
            albumDataGrid.ItemsSource = album;
            return true;
        }

        private bool SearchForSong(string titleToSearchFor)
        {
            var song = SongAccess.DoesSongExist(titleToSearchFor);
            if (song[0] == null) return false;
            var playlist = new ListViewService();
            songOrCdDataGrid.ItemsSource = playlist.CreateViewItem(song);
            return true;
        }

        private void songOrCdDataGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            CellFormating.SupressIdValues(e);
            CellFormating.SpaceOutNames(e);
        }

        public void albumDataGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            CellFormating.SupressIdValues(e);
            CellFormating.SuppressAlbumData(e);
            CellFormating.SpaceOutNames(e);
        }
    }
}
