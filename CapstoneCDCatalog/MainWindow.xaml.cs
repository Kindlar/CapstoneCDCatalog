using System.Windows;

namespace CapstoneCDCatalog
{
    public partial class MainWindow : Window
    {
        public CDCatalogDataAccess Access { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.Access = new CDCatalogDataAccess();
            DisplayGenreList();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var genreToAdd = genreTextBox.Text.Trim();
            if (string.IsNullOrEmpty(genreToAdd))
                MessageBox.Show("Please correct your entry and try again");
            else
                Access.AddGenre(genreToAdd);
            DisplayGenreList();
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            var genreToRemove = genreTextBox.Text.Trim();
            if (string.IsNullOrEmpty(genreToRemove))
                MessageBox.Show("Please correct your entry and try again");
            else
                Access.RemoveGenre(genreToRemove);
            DisplayGenreList();
        }

        private void DisplayGenreList()
        {
            genreListBox.Items.Clear();
            if (Access != null)
            {
                var genrelist = Access.GetGenreList().ToArray();
                foreach (var genre in genrelist)
                {
                    genreListBox.Items.Add(genre.GenreName);
                }
            }
        }
       
    } 
}
