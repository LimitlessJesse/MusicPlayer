using System.ComponentModel.DataAnnotations;

namespace MusicPlayer.Models.ViewModels.Playlist
{
    public class AddPlaylistViewModel
    {
        [Display(Name = "Playlist Name")]
        [Required]
        public string PlaylistName { get; set; }
    }
}
