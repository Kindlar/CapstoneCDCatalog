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
            try
            {
                if (AreSongValuesValid())
                {
                    AddSong();
                    ClearBoxes();
                    songTextBox.Focus();
                }
                else
                    DisplayError();
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

        private void ClearBoxes()
        {
            songTextBox.Text = string.Empty;
            IncreaseTrackNumber();
            trackLengthTextBox.Text = string.Empty;
            songRatingComboBox.Text = string.Empty;
            addSongTextBlock.Text = string.Empty;
        }

        private void IncreaseTrackNumber()
        {
            int trackNumber;
            int.TryParse(trackNumberTextBox.Text, out trackNumber);
            if (trackNumber > 0)
                trackNumberTextBox.Text = (trackNumber + 1).ToString();
        }

        private void AddSong()
        {
            if (!AreSongValuesValid() && !ComboBoxesAreSelected()) return;
            int year, albumRating, songRating;
            int.TryParse(albumRatingComboBox.Text, out albumRating);
            int.TryParse(albumYearTextBox.Text, out year);
            int.TryParse(songRatingComboBox.Text, out songRating);
            Access.AddSong(songTextBox.Text.Trim(), artistTextBox.Text.Trim(), albumTextBox.Text.Trim(),
                trackNumberTextBox.Text.Trim(),
                genreTextBox.Text.Trim(), trackLengthTextBox.Text.Trim(), songRating, year, albumRating);
        }

        private bool ComboBoxesAreSelected()
        {
            return ((int) songRatingComboBox.SelectedItem > 0 && (int) albumRatingComboBox.SelectedItem > 0);
        }

        private bool AreSongValuesValid()
        {
            return !string.IsNullOrEmpty(songTextBox.Text)
                   && !string.IsNullOrEmpty(artistTextBox.Text)
                   && !string.IsNullOrEmpty(albumTextBox.Text)
                   && !string.IsNullOrEmpty(trackNumberTextBox.Text)
                   && !string.IsNullOrEmpty(genreTextBox.Text)
                   && !string.IsNullOrEmpty(trackLengthTextBox.Text)
                   && !string.IsNullOrEmpty(albumYearTextBox.Text)
                   && albumRatingComboBox.SelectedItem != null
                   && songRatingComboBox.SelectedItem != null;
        }

        private void DisplayError()
        {
            addSongTextBlock.Text = "You did not give enough information to add a song.";
        }
    }
}