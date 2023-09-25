using UrlShortener.Models;

namespace UrlShortener.Services
{
    public static class ShortUrlCreatorService
    {
        private static readonly string _pathBase = "abcdefghijklmnopqrstuvwxyz0123456789";
        private static readonly int _pathSize = 5;
        private static readonly string _route = "http://chr.dc/";

        private static readonly Random _res = new Random();

        public static UrlInfo CreateShortUrl(string url)
        {
            return new UrlInfo
            {
                Hits = 0,
                Url = url,
                ShortUrl = _route + GeneratePath()
            };
        }

        public static string GeneratePath()
        {
            string path = "";

            for (int i = 0; i < _pathSize; i++)
            {
                int x = _res.Next(_pathBase.Length);
                path += _pathBase[x];
            }

            return path;
        }
    }
}
