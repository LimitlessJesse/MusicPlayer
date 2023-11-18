using MusicPlayer.Models.DataModels;

namespace MusicPlayer.Models.Database.Interfaces
{
    public interface IPlaylistRepository
    {
        Task<IEnumerable<Playlist>> GetUserAllPlaylistAsync(string userId);
        Task<Playlist> GetCurrentUserPlaylistByIdAsync(int playlistId);

        bool AddPlaylistToCurrentUser(Playlist playlist);
        bool DeletePlaylistFromCurrentUser(Playlist playlist);
    }
}
