using Microsoft.EntityFrameworkCore;
using MusicPlayer.Models.Database.Interfaces;
using MusicPlayer.Models.DataModels;

namespace MusicPlayer.Models.Database.Repository
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly MusicPlayerDbContext _dbContext;

        public PlaylistRepository(MusicPlayerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Playlist>> GetUserAllPlaylistAsync(string userId)
        {
            return await _dbContext.Playlists.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<Playlist> GetUserPlaylistByIdAsync(string userId, int playlistId)
        {
            return await _dbContext.Playlists.FirstOrDefaultAsync(p => p.UserId == userId && p.PlaylistId == playlistId);
        }

        public bool AddPlaylistToCurrentUser(Playlist playlist)
        {
            _dbContext.Playlists.Add(playlist);
            return SaveToDatabase();
        }

        public bool DeletePlaylistFromCurrentUser(Playlist playlist)
        {
            _dbContext.Playlists.Remove(playlist);
            return SaveToDatabase();
        }

        private bool SaveToDatabase()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0;
        }
    }
}
