//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CapstoneCDCatalog
{
    using System;
    
    public partial class Song
    {
        public int SongID { get; set; }
        public string SongTitle { get; set; }
        public int ArtistId { get; set; }
        public int TrackNumber { get; set; }
        public int GenreId { get; set; }
        public int TrackLengthSeconds { get; set; }
        public int SongRating { get; set; }
        public Nullable<int> AlbumId { get; set; }
    
        public virtual Album Album { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
