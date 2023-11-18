namespace MusicPlayer.Services
{
    public class Utils
    {
        private const string YOUTUBE_STARTER = "https://www.youtube.com/watch?v=";
        private const int SOURCEID_LENGTH = 11;

        public static string SongSourceIdExtrator(string embedUrl)
        {
            return embedUrl.Substring(embedUrl.IndexOf(YOUTUBE_STARTER) + YOUTUBE_STARTER.Length, SOURCEID_LENGTH);
        }

        public static string EmbedUrlBuilder(string songSourceId)
        {
            return "https://www.youtube.com/embed/" + songSourceId;
        }
    }
}
