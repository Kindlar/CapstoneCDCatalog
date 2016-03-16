using System.Collections.Generic;
using System.Linq;
using System.Windows;
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
            ListViewService playlist = new ListViewService();
            playListDataGrid.ItemsSource = string.Empty;

            var play = PlayList.GetRandomSongList(seconds);
            List<AlbumSongView> playListView = play.Select(song => playlist.CreateViewItem(song)).ToList();
            playListDataGrid.ItemsSource = playListView;
        }

        private void playListDataGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            CellFormating.SupressIdValues(e);
            CellFormating.SpaceOutNames(e);
        }
    }
}
