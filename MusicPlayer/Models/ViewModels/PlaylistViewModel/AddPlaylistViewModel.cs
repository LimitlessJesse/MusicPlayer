using System.ComponentModel.DataAnnotations;

namespace MusicPlayer.Models.ViewModels.PlaylistViewModel
{
    public class AddPlaylistViewModel
    {
        [Display(Name = "Playlist Name")]
        [Required]
        public string PlaylistName { get; set; }
    }
}
