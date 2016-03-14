using System.Collections.Generic;
using System.Windows;
using CapstoneCDCatalog.Services;

namespace CapstoneCDCatalog
{
    public partial class FindByGenreWindow : Window
    {
        public SongService SongAccess { get; set; }

        public FindByGenreWindow()
        {
            InitializeComponent();
            SongAccess = new SongService();
            PopulateGenres();
        }

        private void PopulateGenres()
        {
            List<Genre> Genrelist = SongAccess.GenreService.GetGenreList();

            foreach (var genre in Genrelist)
            {
                findByGenreComboBox.Items.Add(genre.GenreName);
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = findByGenreComboBox.Text;
            if (selectedItem != null)
            {
                findSongsByGenreDataGrid.ItemsSource = SongAccess.GenreService.GetSongListByGenre(selectedItem);
                findAlbumsByGenreDataGrid.ItemsSource = SongAccess.GenreService.GetAlbumListByGenre(selectedItem);
            }
        }

        private void findSongsByGenreDataGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "SongID" || e.PropertyName == "GenreId" || e.PropertyName == "AlbumId" || e.PropertyName == "ArtistId")
            {
                e.Cancel = true;
            }
        }

        private void findAlbumsByGenreDataGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "SongID" || e.PropertyName == "GenreId" || e.PropertyName == "AlbumId" || e.PropertyName == "ArtistId")
            {
                e.Cancel = true;
            }
        }
    }
}
