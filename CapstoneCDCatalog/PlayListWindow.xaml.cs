using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using CapstoneCDCatalog.Services;

namespace CapstoneCDCatalog
{

    public partial class PlayListWindow : Window
    {
        public PlayList PlayList;
        public PlayListWindow()
        {
            InitializeComponent();
            PlayList = new PlayList();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            int seconds;
            int.TryParse(playListLengthTextBox.Text, out seconds);

            GetPlayListWithSeconds(seconds);
        }

        private void GetPlayListWithSeconds(int seconds)
        {
            PlayListViewService playlist = new PlayListViewService();
            playListDataGrid.ItemsSource = string.Empty;

            var play = PlayList.GetRandomSongList(seconds);
            List<AlbumSongView> thing = play.Select(song => playlist.CreateViewItem(song)).ToList();
            playListDataGrid.ItemsSource = thing;


            //playListDataGrid.ItemsSource = PlayList.GetRandomSongList(seconds);                     
        }

        private void playListDataGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "SongID" || e.PropertyName == "GenreId" || e.PropertyName == "AlbumId" || e.PropertyName == "ArtistId" || e.PropertyName == "SongArtistID")
            {
                e.Cancel = true;
            }

            
        }
    }
}
