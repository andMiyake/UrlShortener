namespace UrlShortener.Contracts.ShortUrl
{
    public record ShortUrlResponse
    (
        int Id,
        int Hits,
        string Url,
        string ShortUrl
    );
}
