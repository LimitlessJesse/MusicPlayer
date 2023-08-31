using System.ComponentModel.DataAnnotations;

namespace MusicPlayer.Models.ViewModels.Playlist
{
    public class DeletePlaylistViewModel
    {
        [Required]
        public int PlaylistIdToDelete;
    }
}
