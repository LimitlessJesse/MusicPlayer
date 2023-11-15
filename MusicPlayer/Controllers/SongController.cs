using Microsoft.AspNetCore.Mvc;
using MusicPlayer.Models.Database.Interfaces;
using MusicPlayer.Models.DataModels;
using MusicPlayer.Models.ViewModels;
using MusicPlayer.Models.ViewModels.SongViewModel;
using MusicPlayer.Services;
using System.Security.Claims;

namespace MusicPlayer.Controllers
{
    public class SongController : Controller
    {
        private readonly ISongRepository _songRepository;

        public SongController(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public async Task<IActionResult> Index(int id)
        {
            var songs = await _songRepository.GetAllSongByPlaylistIdAsync(id);
            var response = new SongIndexViewModel()
            {
                Songs = songs,
                PlaylistId = id
            };
            return View(response);
        }

        [HttpGet]
        public IActionResult AddSong(int id)
        {
            var response = new AddSongViewModel()
            {
                PlaylistId = id
            };
            return PartialView(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddSong(AddSongViewModel addsongViewModel, int id)
        {
            // The id from the parameter is required because the PlaylistId in addsongViewModel is not inputted by the user, 
            // it will contain the default value, which is 0.
            var newSong = new Song()
            {
                SourceId = Utils.SongSourceIdExtrator(addsongViewModel.Source),
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                SongName = addsongViewModel.SongName,
                Artist = addsongViewModel.Artist,
            };
            await _songRepository.AddSongToPlaylistAsync(newSong, id);
            return RedirectToAction("Index", "Song", new { id });
        }

        [HttpGet]
        public IActionResult RemoveSong(string sourceId, int playlistId)
        {
            var response = new RemoveSongViewModel()
            {
                SourceId = sourceId,
                PlaylistId = playlistId
            };
            return PartialView(response);
        }

        public async Task<IActionResult> RemoveSongFromPlaylist(string sourceId, int playlistId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var song = await _songRepository.GetSongAsync(sourceId, currentUserId);
            await _songRepository.RemoveSongFromPlaylistAsync(song, playlistId, currentUserId);
            return RedirectToAction("Index", "Song", new { id=playlistId });
        }

        public IActionResult ChangeSong(string sourceId)
        {
            var model = new VideoPlayerPartialViewModel
            {
                Url = sourceId
            };
            return PartialView("_VideoPlayerPartialView",model);
        }

        // Empty string indicate reach the end of the playlist, doesn't play next song (default mode)
        public async Task<string> PlayNextSong(string sourceId, int playlistId, int playMode)
        {
            // Skip the query to DB so that it runs faster
            if(playMode.Equals(SongPlayMode.SingleSongLoop))
            {
                return sourceId;
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var song = await _songRepository.GetNextSongAsync(playlistId, sourceId, currentUserId, (SongPlayMode)playMode);
            return song == null ? "" : song.SourceId;
        }
    }
}
