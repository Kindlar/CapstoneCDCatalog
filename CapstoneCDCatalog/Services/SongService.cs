﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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

        //public List<AlbumSongView> GetView(string artistName)
        //{
        //    List <AlbumSongView> albumSongViews = new List<AlbumSongView>();
        //    using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
        //    {
        //        db.AlbumSongViews;
        //        return albumSongViews = db.AlbumSongViews.Where(x => x.ArtistName == artistName).ToList();
        //    }
        //}

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
                    Song song = new Song
                    {
                        SongTitle = songToAdd,
                        ArtistId = ArtistService.GetArtistID(artist),
                        AlbumId = AlbumService.GetAlbumID(album, albumYear, albumRating, artist),
                        GenreId = GenreService.GetGenreID(genre),
                        TrackNumber = Convert.ToInt32(track),
                        TrackLengthSeconds = Convert.ToInt32(trackLength),
                        SongRating = songRating
                    };
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
                using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
                {
                    var song = db.Songs.FirstOrDefault(x => x.SongTitle.ToLowerInvariant() == songToAdd.ToLowerInvariant() 
                                                         && x.Album.AlbumTitle.ToLowerInvariant() == album.ToLowerInvariant());
                    if (song != null) result = true;
                }
            }
            return result;
        }

        public void UpdateSongRating(string song, string album, int rating)
        {
            using (CapstoneCDCatalogEntities db = new CapstoneCDCatalogEntities())
            {
                if (DoesSongExist(song, album))
                {
                    var songToUpdate = db.Songs.FirstOrDefault(x => x.SongTitle == song);
                    songToUpdate.SongRating = rating;
                    db.SaveChanges();
                }
            }
        }
    }
}