using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicPlayer.Models.DataModels
{
    public class Song
    {
        [Key, Column(Order = 0)]
        public string SourceId { get; set; }

        [Key, Column(Order = 1)]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public string SongName { get; set; }

        [Required]
        public string Artist { get; set; }

        public List<Playlist> Playlists { get; } = new();

    }
}
