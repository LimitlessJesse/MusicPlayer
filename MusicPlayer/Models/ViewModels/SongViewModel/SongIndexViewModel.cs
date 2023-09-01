using MusicPlayer.Models.DataModels;
using System.ComponentModel.DataAnnotations;

namespace MusicPlayer.Models.ViewModels.SongViewModel
{
    public class SongIndexViewModel
    {
        [Required]
        public IEnumerable<Song> Songs { get; set; }

        [Required]
        public int PlaylistId { get; set; }
    }
}
