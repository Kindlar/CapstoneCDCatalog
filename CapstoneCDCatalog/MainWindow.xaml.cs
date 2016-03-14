using System.Windows;
using CapstoneCDCatalog.Services;

namespace CapstoneCDCatalog
{
    public partial class MainWindow : Window
    {
        public SongService Access { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Access = new SongService();
            DisplayListBoxes();
        }

        private void displayButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayListBoxes();
        }

        private void DisplayListBoxes()
        {
            DisplayGenreList();
            DisplayArtistList();
            DisplayAlbumList();
            DisplaySongList();
        }

        private void DisplayAlbumList()
        {
           albumListBox.Items.Clear();
            if (Access != null)
            {
                var albumList = Access.AlbumService.GetAlbumList().ToArray();
                foreach (var album in albumList)
                {
                    albumListBox.Items.Add(album.AlbumTitle);
                }
            }
        }

        private void DisplaySongList()
        {
            if (Access != null)
            {
                var songList = Access.GetSongList().ToArray();
                DisplaySong(songList);
            }
        }

        private void DisplayGenreList()
        {
            genreListBox.Items.Clear();
            if (Access != null)
            {
                var genrelist = Access.GenreService.GetGenreList().ToArray();
                foreach (var genre in genrelist)
                {
                    genreListBox.Items.Add(genre.GenreName);
                }
            }
        }

        private void DisplayArtistList()
        {
            artistListBox.Items.Clear();
            if (Access != null)
            {
                var artistList = Access.ArtistService.GetArtistList().ToArray();
                foreach (var artist in artistList)
                {
                    artistListBox.Items.Add(artist.ArtistName);
                }
            }
        }

        private void albumListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (albumListBox.SelectedItem != null) DisplaySongsByOnThisAlbum(albumListBox.SelectedItem.ToString());
        }

        private void DisplaySongsByOnThisAlbum(string selectedItems)
        {
            if (Access != null)
            {
                var songList = Access.GetSongListByAlbum(selectedItems).ToArray();
                DisplaySong(songList);
            }           
        }

        private void genreListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (genreListBox.SelectedItem != null)
            {
                DisplaySongsByGenre(genreListBox.SelectedItem.ToString());                
            }
        }

        private void DisplaySongsByGenre(string selectedItem)
        {
            if (Access != null)
            {
                var songList = Access.GenreService.GetSongListByGenre(selectedItem).ToArray();
                DisplaySong(songList);
            }
        }

        private void DisplaySong(Song[] songList)
        {
            songListBox.Items.Clear();
            foreach (var song in songList)
            {
                songListBox.Items.Add(song.SongTitle);
            }
        }

        private void artistListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (artistListBox.SelectedItem != null) DisplaySongsByArtist(artistListBox.SelectedItem.ToString());
        }

        private void DisplaySongsByArtist(string selectedItem)
        {
            if (Access != null)
            {
                var songList = Access.GetSongListByArtist(selectedItem).ToArray();
                DisplaySong(songList);
            }
        }

        private static void DisplayError()
        {
            MessageBox.Show("You did not give enough information to add a song or an album");
        }

        private void goToPlayListButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new PlayListWindow();
            window.Show();
        }

        private void findCdOrSongByTitleButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new FindByTitleWindow();
            window.Show();
        }

        private void findCdOrSongByArtistButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new FindByArtistWindow();
            window.Show();
        }

        private void findCdOrSongByGenreButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new FindByGenreWindow();
            window.Show();
        }

        private void updateRatingsButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new UpdateRatingWindow();
            window.Show();
        }

        private void addAlbumButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddAlbumWindow();
            window.Show();
        }

        private void addSongButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddSongWindow();
            window.Show();
        }
    } 
}