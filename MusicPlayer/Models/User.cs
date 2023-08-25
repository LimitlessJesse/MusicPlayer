using System.ComponentModel.DataAnnotations;

namespace MusicPlayer.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        // One user can have multiple playlists
        // Ref: https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many
        public ICollection<Playlist> Playlists { get; } = new List<Playlist>();
    }
}
