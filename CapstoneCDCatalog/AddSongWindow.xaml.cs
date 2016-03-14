using System;
using System.Windows;
using CapstoneCDCatalog.Services;

namespace CapstoneCDCatalog
{
    public partial class AddSongWindow : Window
    {
        public SongService Access { get; set; }

        public AddSongWindow()
        {
            InitializeComponent();
            Access = new SongService();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (AreSongValuesValid())
            {
                AddSong();
                ClearBoxes();
            }
            else
                DisplayError();
        }

        private void ClearBoxes()
        {
            songTextBox.Text = String.Empty;
            IncreaseTrackNumber();
            trackLengthTextBox.Text = String.Empty;
            songRatingComboBox.Text = String.Empty;
        }

        private void IncreaseTrackNumber()
        {
            int trackNumber;
            int.TryParse(trackNumberTextBox.Text, out trackNumber);
            if(trackNumber > 0)
            trackNumberTextBox.Text = (trackNumber + 1).ToString();
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
            if (!string.IsNullOrEmpty(songTextBox.Text)
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

        private void DisplayError()
        {
            addSongTextBlock.Text = "You did not give enough information to add a song.";
            
        }
    }
}
