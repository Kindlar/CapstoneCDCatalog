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
            if(AreTextInputsValid())
            {
                int year, albumRating;
                int.TryParse(albumYearTextBox.Text, out year);
                int.TryParse(albumRatingComboBox.Text, out albumRating);
                var albumTitle = albumTitleTextBox.ToString();

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

        private bool AreTextInputsValid()
        {
            return !string.IsNullOrEmpty(artistTextBox.Text)
                   && !string.IsNullOrEmpty(albumTitleTextBox.Text)
                   && !string.IsNullOrEmpty(albumYearTextBox.Text)
                   && !string.IsNullOrEmpty(albumRatingComboBox.Text);
        }

        private void AddAlbum(string albumTitle, int year, int albumRating)
        {
            Access.AlbumService.AddAlbum(albumTitle, year, albumRating, artistTextBox.Text);
        }
    }
}
