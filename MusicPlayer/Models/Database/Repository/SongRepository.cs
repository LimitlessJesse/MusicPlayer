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
            var songs = from playlist in _dbContext.Playlists where playlist.PlaylistId == playlistId
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

        public async Task<Song> GetNextSongAsync(int playlistId, string sourceId, string userId, SongPlayMode playMode)
        {

            var songs = await GetAllSongByPlaylistIdAsync(playlistId);
            var songList = songs.ToList();
            if (playMode.Equals(SongPlayMode.Default))
            {
                var currSongIndex = songList.FindIndex(s => s.UserId == userId && s.SourceId == sourceId);
                if (currSongIndex != songList.Count)
                {
                    var nextSong = songList[currSongIndex + 1];
                    return nextSong;
                }
                else
                {
                    // null means it reach the end of the playlist, no more song to play
                    return null!;
                }
            }
            else if(playMode.Equals(SongPlayMode.Random))
            {
                Random rand = new Random();
                int nextSongIndex = rand.Next(0, songList.Count);
                return songList[nextSongIndex];
            }
            else if(playMode.Equals(SongPlayMode.PlaylistLoop))
            {
                var currSongIndex = songList.FindIndex(s => s.UserId == userId && s.SourceId == sourceId);
                var nextSong = songList[ (currSongIndex + 1) % songList.Count ];
                return nextSong;
            }
            
            // Curently singleSongLoop playmode is handled in a upper level to avoid multiple calls, so 
            // no condition should end up in here
            return null!;
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

        public async Task<bool> RemoveSongFromPlaylistAsync(Song song, int playlistId, string userId)
        {
            var playlist = await _playlistRepository.GetCurrentUserPlaylistByIdAsync(playlistId);
            playlist.Songs.Remove(song);

            // TODO: Maybe remove the song entity from Song Table if no more reference to this song is found in any playlist?
            var allPlaylists = await _playlistRepository.GetUserAllPlaylistAsync(userId);

            if (!allPlaylists.Any(p => p.Songs.Any(s => s.SourceId == song.SourceId)))
            {
                _dbContext.Songs.Remove(song);
            }

            return SaveToDatabase();
        }

        private bool SaveToDatabase()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0;
        }
    }
}
