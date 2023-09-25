using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortener.Models
{
    public class UrlInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Hits { get; set; }

        [Required]
        [MaxLength(200)]
        public string Url { get; set; } = "";

        [Required]
        [MaxLength(30)]
        public string ShortUrl { get; set; } = "";

        public UrlInfo()
        {
        }

        public UrlInfo(int id, int hits, string url, string shortUrl)
        {
            Id = id;
            Hits = hits;
            Url = url;
            ShortUrl = shortUrl;
        }
    }
}