using System;
using System.Windows;
using CapstoneCDCatalog.Services;

namespace CapstoneCDCatalog
{
    public partial class AddAlbumWindow : Window
    {
        public SongService Access { get; set; }

        public AddAlbumWindow()
        {
            InitializeComponent();
            Access = new SongService();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AreTextInputsValid())
                {
                    int year, albumRating;
                    int.TryParse(albumYearTextBox.Text, out year);
                    int.TryParse(albumRatingComboBox.Text, out albumRating);
                    var albumTitle = albumTitleTextBox.Text;

                    if (Access.AlbumService.DoesAlbumExists(albumTitle, albumRating))
                    {
                        addAlbumTextBlock.Text = "Album already is present in catalog";
                    }
                    else
                    {
                        AddAlbum(albumTitle, year, albumRating);
                        addAlbumTextBlock.Text = "The album has been added!";
                    }
                }
                else
                {
                    addAlbumTextBlock.Text = "Please verify your inputs and try again";
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

        private bool AreTextInputsValid()
        {
            try
            {
                return !string.IsNullOrEmpty(artistTextBox.Text)
                       && !string.IsNullOrEmpty(albumTitleTextBox.Text)
                       && !string.IsNullOrEmpty(albumYearTextBox.Text)
                       && !string.IsNullOrEmpty(albumRatingComboBox.Text);
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show($"Please check all your unputs are valid: {ex}");
                return false;
            }
        }

        private void AddAlbum(string albumTitle, int year, int albumRating)
        {
            Access.AlbumService.AddAlbum(albumTitle, year, albumRating, artistTextBox.Text);
        }
    }
}