using System;
using System.Collections.Generic;
using System.Windows;
using CapstoneCDCatalog.Services;

namespace CapstoneCDCatalog
{

    public partial class UpdateRatingWindow : Window
    {
        public SongService SongAccess { get; set; }

        public UpdateRatingWindow()
        {
            InitializeComponent();
            SongAccess = new SongService();
            PopulateAlbumComboBox();
        }

        private void PopulateAlbumComboBox()
        {
            List<Album> Albumlist = SongAccess.AlbumService.GetAlbumList();

            foreach (var album in Albumlist)
            {
                albumComboBox.Items.Add(album.AlbumTitle);
            }
        }

        private void getCurrentRatingButton_Click(object sender, RoutedEventArgs e)
        {
            updateAlbumRatingTextBlock.Text = String.Empty;
            string albumToRate = albumComboBox.SelectedItem.ToString();
            if (!string.IsNullOrEmpty(albumToRate))
            {
                var album = SongAccess.AlbumService.GetAlbum(albumToRate);
                albumRatingComboBox.Text = album.AlbumRating.ToString();
            }        
        }

        private void updateAlbumRatingButton_Click(object sender, RoutedEventArgs e)
        {
            int newRating;
            int.TryParse(albumRatingComboBox.Text, out newRating);
            string albumToRate = albumComboBox.SelectedItem.ToString();

            if (!string.IsNullOrEmpty(albumToRate))
            {
                SongAccess.AlbumService.UpdateAlbumRating(albumToRate, newRating);
                albumRatingComboBox.Text = String.Empty;
                albumComboBox.Text = String.Empty;
                updateAlbumRatingTextBlock.Text = "The rating has been updated!";
            }
            else
            {
                updateAlbumRatingTextBlock.Text = "There was a problem, please try again.";
            }
        }

        private void searchForSongButton_Click(object sender, RoutedEventArgs e)
        {
            songRatingComboBox.Text = String.Empty;
            string songToRate = songToUpdateTextBox.Text;
            string albumOfsong = songAlbumRatingTextBox.Text;
            if (!string.IsNullOrEmpty(songToRate) && !string.IsNullOrEmpty(albumOfsong))
            {
                if (SongAccess.DoesSongExist(songToRate, albumOfsong))
                {
                    var song = SongAccess.GetSong(songToRate, albumOfsong);
                    songRatingComboBox.Text = song.SongRating.ToString();


                }
                else
                {
                    updateSongRatingTextBlock.Text = "That pair of song and album was not found.";
                }
            }
            else
            {
                updateSongRatingTextBlock.Text = "Please enter a song to update.";
            }
        }

        private void updateSongRatingButton_Click(object sender, RoutedEventArgs e)
        {
            int newRating;
            int.TryParse(songRatingComboBox.Text, out newRating);
            string songToRate = songToUpdateTextBox.Text;
            string albumToRate = songAlbumRatingTextBox.Text;

            if (!string.IsNullOrEmpty(songToRate) && !string.IsNullOrEmpty(albumToRate))
            {
                SongAccess.UpdateSongRating(songToRate, albumToRate, newRating);
                updateSongRatingTextBlock.Text = "Song rating has been changed!";
                songToUpdateTextBox.Text = String.Empty;
                songRatingComboBox.Text = String.Empty;
                songAlbumRatingTextBox.Text = String.Empty;
            }
            else
            {
                updateSongRatingTextBlock.Text = "There was a problem, please try again.";
            }
        }
    }
}
