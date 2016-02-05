using System.Windows;
using CapstoneCDCatalog.Services;

namespace CapstoneCDCatalog
{
    public partial class MainWindow
    {
        public SongService Access { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Access = new SongService();
            DisplayListBoxes();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if(AreSongValuesValid())
                AddSong();
            else if (AreAlbumValuesValid())
                AddAlbum();
            else
                DisplayError();
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
            //DisplayView();
        }

        private void DisplayView()
        {
           
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

        private void AddSong()
        {
            if (AreSongValuesValid())
            {
                int year, albumRating, songRating;
                int.TryParse(albumRatingComboBox.Text, out albumRating); 
                int.TryParse(albumYearTextBox.Text, out year);
                int.TryParse(songRatingComboBox.Text, out songRating);
                Access.AddSong(songTextBox.Text.Trim(), songTextBox.Text.Trim(), albumTextBox.Text.Trim(), trackNumberTextBox.Text.Trim(), 
                    genreTextBox.Text.Trim(), trackLengthTextBox.Text.Trim(), songRating, year,
                    albumRating);
            }
        }

        private bool AreSongValuesValid()
        {
            if(!string.IsNullOrEmpty(songTextBox.Text) 
                && !string.IsNullOrEmpty(artistTextBox.Text) 
                && !string.IsNullOrEmpty(albumTextBox.Text)
                && !string.IsNullOrEmpty(trackNumberTextBox.Text) 
                && !string.IsNullOrEmpty(genreTextBox.Text)
                && !string.IsNullOrEmpty(trackLengthTextBox.Text) 
                && !string.IsNullOrEmpty(songRatingComboBox.SelectedItem.ToString())
                && !string.IsNullOrEmpty(albumYearTextBox.Text)
                && !string.IsNullOrEmpty(albumRatingComboBox.SelectedItem.ToString()))
            return true;
            return false;
        }

        private void AddAlbum()
        {
            int year, albumRating;
            int.TryParse(albumYearTextBox.Text, out year);
            int.TryParse(albumRatingComboBox.Text, out albumRating);

            if (!string.IsNullOrEmpty(albumTextBox.Text) && !string.IsNullOrEmpty(albumYearTextBox.Text) &&
                !string.IsNullOrEmpty(albumRatingComboBox.SelectedItem.ToString()) && !string.IsNullOrEmpty(artistTextBox.Text))
                Access.AlbumService.AddAlbum(albumTextBox.Text, year, albumRating, artistTextBox.Text);            
        }
        private bool AreAlbumValuesValid()
        {
            if (!string.IsNullOrEmpty(artistTextBox.Text)
                && !string.IsNullOrEmpty(albumTextBox.Text)
                && !string.IsNullOrEmpty(albumYearTextBox.Text)
                && !string.IsNullOrEmpty(albumRatingComboBox.SelectedItem.ToString()))
                return true;
            return false;
        }

        private static void DisplayError()
        {
            MessageBox.Show("You did not give enough information to add a song or an album");
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            int rating;
            int.TryParse(songRatingComboBox.Text, out rating);
            if(songRatingComboBox.SelectedItem != null 
                && !string.IsNullOrEmpty(albumTextBox.Text) 
                && !string.IsNullOrEmpty(songTextBox.Text))
                Access.UpdateSongRating(songTextBox.Text, albumTextBox.Text, rating);
        }
    } 
}