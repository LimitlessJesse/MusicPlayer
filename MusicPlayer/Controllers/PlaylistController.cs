using Microsoft.AspNetCore.Mvc;
using MusicPlayer.Models.Database.Interfaces;
using MusicPlayer.Models.DataModels;
using MusicPlayer.Models.ViewModels.Playlist;
using System.Security.Claims;

namespace MusicPlayer.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly IPlaylistRepository _playlistRepository;

        public PlaylistController(IPlaylistRepository playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var playlists = await _playlistRepository.GetUserAllPlaylistAsync(currentUserId);
            return View(playlists);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var response = new AddPlaylistViewModel();
            return PartialView(response);
        }

        [HttpPost]
        public IActionResult Create(AddPlaylistViewModel addPlaylistViewModel)
        {
            var newPlaylistName = addPlaylistViewModel.PlaylistName;
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _playlistRepository.AddPlaylistToCurrentUser(new Playlist
            {
                PlaylistName = newPlaylistName,
                UserId = currentUserId
            });

            return RedirectToAction("Index", "Playlist");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var response = new DeletePlaylistViewModel
            {
                PlaylistIdToDelete = id
            };
            return PartialView(response);
        }

        public async Task<IActionResult> DeletePlaylist(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var playlist = await _playlistRepository.GetUserPlaylistByIdAsync(currentUserId, id);
            _playlistRepository.DeletePlaylistFromCurrentUser(playlist);
            return RedirectToAction("Index", "Playlist");
        }
    }
}
