using System.ComponentModel.DataAnnotations;

namespace MusicPlayer.Models
{
    public class Playlist
    {
        [Key]
        public int PlaylistId { get; set; }

        [Required]
        public string PlaylistName { get; set; }

        public List<Song> Songs { get; } = new ();

        // Required one-to-many relationship: Playlist must belong to a user
        // Ref: https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many
        public int UserId { get; set; }             // Required foreign key property
        public User User { get; set; } = null!;     // Required reference navigation to principal
    }
}
