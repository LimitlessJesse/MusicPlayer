using System.ComponentModel.DataAnnotations;

namespace MusicPlayer.Models
{
    public class Song
    {
        [Key]
        public string SourceId { get; set; }

        [Required]
        public string SongName { get; set; }

        [Required]
        public string Artist { get; set; }

        public List<Playlist> Playlists { get; } = new();

    }
}
