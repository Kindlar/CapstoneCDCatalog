using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            this.Access = new SongService();
            DisplayListBoxes();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(genreTextBox.Text)) AddGenre(); 
            if(!string.IsNullOrEmpty(artistTextBox.Text)) AddArtist();
            if(!string.IsNullOrEmpty(albumTextBox.Text)) AddAlbum();
            if (!string.IsNullOrEmpty(songTextBox.Text)) AddSong();
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            var genreToRemove = genreTextBox.Text.Trim();
            if (string.IsNullOrEmpty(genreToRemove))
                MessageBox.Show("Please correct your entry and try again");
            else
                Access.GenreService.RemoveGenre(genreToRemove);
            DisplayGenreList();
        }

        private void findButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void displayButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayListBoxes();
        }

        private void DisplayListBoxes()
        {
            DisplayArtistList();
            DisplayGenreList();
            DisplaySongList();
            DisplayAlbumList();
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

        private void AddGenre()
        { 
            Access.GenreService.AddGenre(genreTextBox.Text.Trim());
            DisplayGenreList();
        }

        private void AddArtist()
        {
            Access.ArtistService.AddArtist(artistTextBox.Text.Trim());
            DisplayArtistList();
        }

        private void AddSong()
        {
            throw new System.NotImplementedException();
        }

        private void AddAlbum()
        {
            if (!string.IsNullOrEmpty(albumTextBox.Text) && !string.IsNullOrEmpty(albumYearTextBox.Text) &&
                !string.IsNullOrEmpty(albumRatingComboBox.SelectedItem.ToString()) && !string.IsNullOrEmpty(artistTextBox.Text))
                Access.AlbumService.AddAlbum(albumTextBox.Text, albumYearTextBox.Text, albumRatingComboBox.SelectedItem.ToString(), artistTextBox.Text);            
        }
    } 
}
