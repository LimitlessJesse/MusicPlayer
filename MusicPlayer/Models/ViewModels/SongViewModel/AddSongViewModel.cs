using System.ComponentModel.DataAnnotations;

namespace MusicPlayer.Models.ViewModels.SongViewModel
{
    public class AddSongViewModel
    {
        [Display(Name = "Song link")]
        [Required]
        public string Source { get; set; }

        [Display(Name = "Song Name")]
        [Required]
        public string SongName { get; set; }

        [Display(Name = "Artist Name")]
        [Required]
        public string Artist { get; set; }

        public int PlaylistId { get; set; }
    }
}
