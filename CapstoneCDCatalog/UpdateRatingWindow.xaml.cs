using System;
using System.Collections.Generic;
using System.Windows;
using CapstoneCDCatalog.Services;

namespace CapstoneCDCatalog
{
    public partial class UpdateRatingWindow : Window
    {
        public SongService SongAccess { get; set; } = new SongService();

        public UpdateRatingWindow()
        {
            InitializeComponent();
            PopulateAlbumComboBox();
        }

        private void PopulateAlbumComboBox()
        {
            List<Album> albumlist = SongAccess.AlbumService.GetAlbumList();

            foreach (var album in albumlist)
            {
                albumComboBox.Items.Add(album.AlbumTitle);
            }
        }

        private void getCurrentRatingButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                updateAlbumRatingTextBlock.Text = string.Empty;
                string albumToRate = albumComboBox.SelectedItem.ToString();
                if (string.IsNullOrEmpty(albumToRate)) return;
                var album = SongAccess.AlbumService.GetAlbum(albumToRate);
                albumRatingComboBox.Text = album.AlbumRating.ToString();
            }
            catch (NullReferenceException ex)
            {
                DisplayExceptions.DisplayNullReference(ex);
            }
            catch (Exception ex)
            {
                DisplayExceptions.DisplayException(ex);
            }
        }

        private void updateAlbumRatingButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int newRating;
                int.TryParse(albumRatingComboBox.Text, out newRating);
                string albumToRate = albumComboBox.SelectedItem.ToString();

                if (!string.IsNullOrEmpty(albumToRate))
                {
                    SongAccess.AlbumService.UpdateAlbumRating(albumToRate, newRating);
                    albumRatingComboBox.Text = string.Empty;
                    albumComboBox.Text = string.Empty;
                    updateAlbumRatingTextBlock.Text = "The rating has been updated!";
                }
                else
                {
                    updateAlbumRatingTextBlock.Text = "There was a problem, please try again.";
                }
            }
            catch (NullReferenceException ex)
            {
                DisplayExceptions.DisplayNullReference(ex);
            }
            catch (Exception ex)
            {
                DisplayExceptions.DisplayException(ex);
            }
        }

        private void searchForSongButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                songRatingComboBox.Text = string.Empty;
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
            catch (NullReferenceException ex)
            {
                DisplayExceptions.DisplayNullReference(ex);
            }
            catch (Exception ex)
            {
                DisplayExceptions.DisplayException(ex);
            }
        }

        private void updateSongRatingButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int newRating;
                int.TryParse(songRatingComboBox.Text, out newRating);
                string songToRate = songToUpdateTextBox.Text;
                string albumToRate = songAlbumRatingTextBox.Text;

                if (!string.IsNullOrEmpty(songToRate) && !string.IsNullOrEmpty(albumToRate))
                {
                    SongAccess.UpdateSongRating(songToRate, albumToRate, newRating);
                    updateSongRatingTextBlock.Text = "Song rating has been changed!";
                    songToUpdateTextBox.Text = string.Empty;
                    songRatingComboBox.Text = string.Empty;
                    songAlbumRatingTextBox.Text = string.Empty;
                }
                else
                {
                    updateSongRatingTextBlock.Text = "There was a problem, please try again.";
                }
            }
            catch (NullReferenceException ex)
            {
                DisplayExceptions.DisplayNullReference(ex);
            }
            catch (Exception ex)
            {
                DisplayExceptions.DisplayException(ex);
            }
        }
    }
}