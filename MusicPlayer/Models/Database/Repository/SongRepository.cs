using Microsoft.EntityFrameworkCore;
using MusicPlayer.Models.Database.Interfaces;
using MusicPlayer.Models.DataModels;
using System.Security.Claims;

namespace MusicPlayer.Models.Database.Repository
{
    public class SongRepository : ISongRepository
    {
        private readonly MusicPlayerDbContext _dbContext;
        private readonly IPlaylistRepository _playlistRepository;

        public SongRepository(MusicPlayerDbContext dbContext, IPlaylistRepository playlistRepository)
        {
            _dbContext = dbContext;
            _playlistRepository = playlistRepository;
        }

        public async Task<IEnumerable<Song>> GetAllSongByPlaylistIdAsync(int playlistId)
        {
            var songs = from playlist in _dbContext.Playlists
                        join playlistSong in _dbContext.PlaylistsSong on playlist.PlaylistId equals playlistSong.PlaylistId
                        join song in _dbContext.Songs on new { Source = playlistSong.SongSourceId, Owner = playlistSong.SongUserId }
                             equals new { Source = song.SourceId, Owner = song.UserId }
                        orderby playlistSong.InsertedOn
                        select song;

            return await songs.ToListAsync();
        }

        public async Task<Song> GetSongAsync(string sourceId, string userId)
        {
            return await _dbContext.Songs.FirstOrDefaultAsync(s => s.SourceId == sourceId && s.UserId == userId);
        }

        public async Task<bool> AddSongToPlaylistAsync(Song song, int playlistIdToAdd)
        {
            var songExist = _dbContext.Songs.Any(s => s.SourceId == song.SourceId && s.UserId == song.UserId);
            var playlist = await _playlistRepository.GetCurrentUserPlaylistByIdAsync(playlistIdToAdd);

            if (songExist)
            {
                var songExistedInPlaylist = playlist.Songs.Any(s => s.SourceId == song.SourceId && s.UserId == song.SourceId);
                if ( !songExistedInPlaylist ) 
                {
                    // DO NOT just try to use Songs.Add(song). Getting the Song Entity from DB is necessary
                    // Or adding duplicate entity exception will be thrown 
                    var existedSongEntity = await GetSongAsync(song.SourceId, song.UserId);
                    playlist.Songs.Add(existedSongEntity); 
                }
                else
                {
                    // TODO: alert song already exist in playlist
                }
            }
            else
            {
                _dbContext.Songs.Add(song);
                playlist.Songs.Add(song);
            }
            return SaveToDatabase();
        }

        public async Task<bool> RemoveSongFromPlaylistAsync(Song song, int playlistId)
        {
            var playlist = await _playlistRepository.GetCurrentUserPlaylistByIdAsync(playlistId);
            playlist.Songs.Remove(song);

            // TODO: Maybe remove the song entity from Song Table if no more reference to this song is found in any playlist?
            return SaveToDatabase();
        }

        private bool SaveToDatabase()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0;
        }
    }
}
