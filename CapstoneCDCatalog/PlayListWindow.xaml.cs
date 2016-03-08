using System.Windows;

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
            DisplaySongList();
        }

        private void DisplaySongList()
        {
            playListBox.Items.Clear();
            if (PlayList != null)
            {
                foreach (var song in PlayList.ListOfSongs)
                {
                    playListBox.Items.Add(song.SongTitle);
                }
            }
        }

        private void GetPlayListWithSeconds(int seconds)
        {
            PlayList.GetRandomSongList(seconds);
        }
    }
}
