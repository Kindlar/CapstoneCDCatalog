using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CapstoneCDCatalog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddGenre("New Age");
        }

        private void AddGenre(string genreToAdd)
        {
            using (CDCatalogEntities db = new CDCatalogEntities())
            {
                var genreList = db.Genres.ToList();

                Genre genre = new Genre();
                genre.GenreId = 0;
                genre.GenreName = genreToAdd;
                db.Genres.Add(genre);
                db.SaveChanges();
            }
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveGenre("New Age");
        }

        private void RemoveGenre(string genreToRemove)
        {
            using (CDCatalogEntities db = new CDCatalogEntities())
            {
                var genreList = db.Genres.ToList();

                Genre genre = new Genre();
                genre.GenreId = 0;
                genre.GenreName = genreToRemove;
                var value = db.Genres.Find(genre.GenreName);
                db.Genres.Remove(value);
                db.SaveChanges();
            }
        }
    } 
}
