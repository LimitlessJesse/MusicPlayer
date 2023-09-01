namespace MusicPlayer.Services
{
    public class Utils
    {
        private const string YOUTUBE_STARTER = "https://www.youtube.com/embed/";
        private const int EMBED_LINK_LENGTH = 31;

        public static string SongSourceIdExtrator(string embedUrl)
        {
            return embedUrl.Substring(embedUrl.IndexOf(YOUTUBE_STARTER) + YOUTUBE_STARTER.Length, EMBED_LINK_LENGTH);
        }

        public static string EmbedUrlBuilder(string songSourceId)
        {
            return "https://www.youtube.com/embed/" + songSourceId;
        }
    }
}
