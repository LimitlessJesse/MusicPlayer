using MusicPlayer.Models.DataModels;

namespace MusicPlayer.Models.Database.Interfaces
{
    public interface ISongRepository
    {
        Task<IEnumerable<Song>> GetAllSongByPlaylistIdAsync(int playlistId);

        Task<Song> GetSongAsync(string sourceId, string userId);

        Task<bool> AddSongToPlaylistAsync(Song song, int playlistIdToAdd);

        Task<bool> RemoveSongFromPlaylistAsync(Song song, int playlistId);

        Task<Song> GetNextSongAsync(int playlistId, string sourceId, string userId, SongPlayMode playMode);
    }
}
