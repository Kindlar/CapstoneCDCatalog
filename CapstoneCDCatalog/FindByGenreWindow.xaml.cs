﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CapstoneCDCatalog.Services;

namespace CapstoneCDCatalog
{
    public partial class FindByGenreWindow : Window
    {
        public SongService SongAccess { get; set; } = new SongService();

        public FindByGenreWindow()
        {
            InitializeComponent();
            PopulateGenres();
        }

        private void PopulateGenres()
        {
            List<Genre> genrelist = SongAccess.GenreService.GetGenreList();

            foreach (var genre in genrelist)
            {
                findByGenreComboBox.Items.Add(genre.GenreName);
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = findByGenreComboBox.Text;
                if (selectedItem != null)
                {
                    ListViewService listView = new ListViewService();
                    List<Song> songListByGenre = SongAccess.GenreService.GetSongListByGenre(selectedItem);
                    List<AlbumSongView> genreListView = songListByGenre.Select(song => listView.CreateViewItem(song)).ToList();

                    findSongsByGenreDataGrid.ItemsSource = genreListView;
                    findAlbumsByGenreDataGrid.ItemsSource = SongAccess.GenreService.GetAlbumListByGenre(selectedItem);
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

        private void findSongsByGenreDataGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            CellFormating.SupressIdValues(e);
            CellFormating.SpaceOutNames(e);
        }

        private void findAlbumsByGenreDataGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            CellFormating.SupressIdValues(e);
            CellFormating.SuppressAlbumData(e);
            CellFormating.SpaceOutNames(e);
        }
    }
}
