using MusicPlayer.Models.DataModels;

namespace MusicPlayer.Models.Database.Interfaces
{
    public interface IPlaylistRepository
    {
        Task<IEnumerable<Playlist>> GetUserAllPlaylistAsync(string userId);
        Task<Playlist> GetUserPlaylistByIdAsync(string userId, int playlistId);

        bool AddPlaylistToCurrentUser(Playlist playlist);
        bool DeletePlaylistFromCurrentUser(Playlist playlist);
    }
}
