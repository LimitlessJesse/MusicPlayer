using System.ComponentModel.DataAnnotations;

namespace MusicPlayer.Models.ViewModels.PlaylistViewModel
{
    public class DeletePlaylistViewModel
    {
        [Required]
        public int PlaylistIdToDelete;
    }
}
