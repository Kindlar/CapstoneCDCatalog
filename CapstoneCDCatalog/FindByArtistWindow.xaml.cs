using System.Collections.Generic;
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
            List<Artist> Artistlist = SongAccess.ArtistService.GetArtistList();

            foreach (var artist in Artistlist)
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
                findSongsByArtistDataGrid.ItemsSource = SongAccess.GetSongListByArtist(selectItem);
                findCdsByArtistDataGrid.ItemsSource = SongAccess.AlbumService.GetAlbumsByArtist(selectItem);
            }
        }

        private void findSongsByArtistDataGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "SongID" || e.PropertyName == "GenreId" || e.PropertyName == "AlbumId" || e.PropertyName == "ArtistId")
            {
                e.Cancel = true;
            }
        }

        private void findCdsByArtistDataGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "SongID" || e.PropertyName == "GenreId" || e.PropertyName == "AlbumId" || e.PropertyName == "ArtistId")
            {
                e.Cancel = true;
            }
        }
    }
}
