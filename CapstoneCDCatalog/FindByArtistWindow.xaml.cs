using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CapstoneCDCatalog.Services;

namespace CapstoneCDCatalog
{
    public partial class FindByArtistWindow : Window
    {
        public SongService SongAccess { get; set; }

        public FindByArtistWindow()
        {
            InitializeComponent();
            SongAccess = new SongService();
            PopulateArtistComboBox();
        }

        private void PopulateArtistComboBox()
        {
            List<Artist> artistlist = SongAccess.ArtistService.GetArtistList();

            foreach (var artist in artistlist)
            {
                findByArtistComboBox.Items.Add(artist.ArtistName);
            }
        }

        private void findByArtistComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
   
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            var selectItem = findByArtistComboBox.Text;
            if (selectItem != null)
            {
                ListViewService listView = new ListViewService();
                List<Song> songList = SongAccess.GetSongListByArtist(selectItem);
                List<AlbumSongView> playListView = songList.Select(song => listView.CreateViewItem(song)).ToList();

                findSongsByArtistDataGrid.ItemsSource = playListView;
                findCdsByArtistDataGrid.ItemsSource = SongAccess.AlbumService.GetAlbumsByArtist(selectItem);
            }
        }

        private void findSongsByArtistDataGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            CellFormating.SupressIdValues(e);
            CellFormating.SpaceOutNames(e);
        }

        private void findCdsByArtistDataGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            CellFormating.SupressIdValues(e);
            CellFormating.SuppressAlbumData(e);
            CellFormating.SpaceOutNames(e);
        }
    }
}
