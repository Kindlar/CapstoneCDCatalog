using System;
using System.Collections.Generic;
using CapstoneCDCatalog.Services;

namespace CapstoneCDCatalog
{
    public class PlayList
    {
        public SongService SongService { get; } = new SongService();
        public List<Song> ListOfSongs = new List<Song>();
        public List<Song> AllSongFromDatabase = new List<Song>();
        public Random Random = new Random();
        public int LengthOfPlaylist { get; set; }

        public PlayList()
        {
            GetAllSongs();
        }

        public void GetAllSongs()
        {
            AllSongFromDatabase = SongService.GetSongList();
        }

        public Song GetRandomSong()
        {
            int randomSong = Random.Next(0, AllSongFromDatabase.Count);
            var song = AllSongFromDatabase[randomSong];
            song.Album = SongService.AlbumService.GetAlbum(song.ArtistId);
            return song;
        }

        public List<Song> GetRandomSongList(int seconds)
        {
            ListOfSongs.Clear();
            LengthOfPlaylist = 0;
            while (LengthOfPlaylist < seconds - 60)
            {
                Song song = GetRandomSong();
                if (song.TrackLengthSeconds + LengthOfPlaylist < seconds + 60 )
                {                    
                    ListOfSongs.Add(song);
                    LengthOfPlaylist += song.TrackLengthSeconds;
                } 
            }
            return ListOfSongs;
        }       
    }
}