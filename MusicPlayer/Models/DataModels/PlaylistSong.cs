namespace MusicPlayer.Models.DataModels
{
    public class PlaylistSong
    {
        // The Foreign Key link will be automatically create on DB

        public int PlaylistId { get; set; }

        public string SongSourceId { get; set; }
        public string SongUserId { get; set; }

        public DateTime InsertedOn { get; set; }
    }
}
