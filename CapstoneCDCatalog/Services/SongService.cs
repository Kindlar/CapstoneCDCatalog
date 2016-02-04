using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CapstoneCDCatalog.Services
{
    public class SongService
    {
        public SongService()
        {
            AlbumService = new AlbumService(this);
            ArtistService = new ArtistService();
        }

        public AlbumService AlbumService { get; }
        public ArtistService ArtistService { get; }
        public GenreService GenreService { get; } = new GenreService();

        public List<Song> GetSongList()
        {
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                var songList = db.Songs.ToList();
                return songList;
            }
        }

        public List<Song> GetSongListByAlbum(string selectedItems)
        {
            var albumID = AlbumService.GetAlbumID(selectedItems);
      
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                var songList = db.Songs.Where(x => x.AlbumId.Value == albumID).Select(x => x);
                return songList.ToList();
            }        
        }

        public List<Song> GetSongListByArtist(string selectedItem)
        {
            var artist = ArtistService.GetArtistID(selectedItem);

            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                var songList = db.Songs.Where(x => x.ArtistId == artist).Select(x => x);
                return songList.ToList();
            }
        }

        public void AddSong(string songToAdd, string artist, string album, string track, string genre, string trackLength, int songRating, int albumYear, int albumRating)
        {
            if (!DoesSongExist(songToAdd, album))
            {
                using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
                {
                    Song song = new Song();
                    song.SongTitle = songToAdd;
                    song.ArtistId = ArtistService.GetArtistID(artist);
                    song.AlbumId = AlbumService.GetAlbumID(album, albumYear, albumRating, artist);
                    song.GenreId = GenreService.GetGenreID(genre);
                    song.TrackNumber = Convert.ToInt32(track);
                    song.TrackLengthSeconds = Convert.ToInt32(trackLength);
                    song.SongRating = songRating;
                    db.Songs.Add(song);
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Song already exists.");
            }
        }

        private bool DoesSongExist(string songToAdd, string album)
        {
            bool result = false;
            {
                var songList = GetSongList();
                foreach (var song in songList)
                {
                    if (song.SongTitle == songToAdd && song.Album.AlbumTitle == album)
                        result = true;
                }
            }
            return result;
        }
    }
}