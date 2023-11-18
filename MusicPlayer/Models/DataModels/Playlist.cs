using System.ComponentModel.DataAnnotations;

namespace MusicPlayer.Models.DataModels
{
    public class Playlist
    {
        [Key]
        public int PlaylistId { get; set; }

        [Required]
        public string PlaylistName { get; set; }

        public List<Song> Songs { get; } = new();

        // Required one-to-many relationship: Playlist must belong to a user
        // Ref: https://stackoverflow.com/questions/32594641/how-to-make-one-to-many-relationship-with-asp-identity-users
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
