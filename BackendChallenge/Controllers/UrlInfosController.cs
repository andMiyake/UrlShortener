using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Contracts.ShortUrl;
using UrlShortener.Models;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UrlInfosController : ControllerBase
    {
        private readonly UrlInfoDataContext _dbContext;

        public UrlInfosController(UrlInfoDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        //GET: urlinfos/
        [HttpGet]
        public async Task<ActionResult<List<UrlInfo>>> GetAllUrl()
        {
            if (_dbContext.UrlInfos == null)
            {
                return NotFound();
            }

            var shortUrl = await _dbContext.UrlInfos.ToListAsync();

            if (shortUrl == null)
            {
                return NotFound();
            }

            return shortUrl;
        }

        //GET: urlinfos/1
        [HttpGet("{id}")]
        public async Task<ActionResult<UrlInfo>> GetUrlInfo(int id)
        {
            if (_dbContext.UrlInfos == null)
            {
                return NotFound();
            }

            var shortUrl = await _dbContext.UrlInfos.FindAsync(id);

            if (shortUrl == null)
            {
                return NotFound();
            }

            return shortUrl;
        }

        //POST: urlinfos/createshorturl
        [HttpPost]
        [Route("crtshorturl")]
        public async Task<ActionResult<UrlInfo>> CreateShortUrl(CreateShortUrlRequest request)
        {
            if (string.IsNullOrEmpty(request.Url))
            {
                return BadRequest();
            }

            if (UrlExists(request.Url))
            {
                return Conflict();
            }
            else
            {
                var shortenedUrl = ShortUrlCreatorService.CreateShortUrl(request.Url);
                _dbContext.UrlInfos.Add(shortenedUrl);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUrlInfo), new { id = shortenedUrl.Id }, shortenedUrl);
            }
        }

        //POST: api/ShortUrlInfos
        //[HttpPost]
        //public async Task<ActionResult<UrlInfo>> CreateShortUrl(CreateShortUrlRequest request)
        //{
        //    if (string.IsNullOrEmpty(request.Url))
        //    {
        //        return BadRequest();
        //    }

        //    if (UrlExists(request.Url))
        //    {
        //        var urlInfo = await _dbContext.UrlInfos.
        //            FirstAsync(surl => surl.Url == request.Url);

        //        var response = new ShortUrlResponse(
        //            urlInfo.Id,
        //            urlInfo.Hits,
        //            urlInfo.Url,
        //            urlInfo.ShortUrl
        //        );

        //        return Conflict(nameof(GetUrlInfo), new { id = response.Id }, response);
        //    }
        //    else
        //    {
        //        var shortenedUrl = ShortUrlCreatorService.CreateShortUrl(request.Url);
        //        _dbContext.UrlInfos.Add(shortenedUrl);
        //        await _dbContext.SaveChangesAsync();

        //        return CreatedAtAction(nameof(GetUrlInfo), new { id = shortenedUrl.Id }, shortenedUrl);
        //    }
        //}

        ////PUT: api/ShortUrlInfos/1
        //[HttpPut("{id}")]
        //public async Task<ActionResult<UrlInfo>> PutShortUrl(int id, UrlInfo shortUrlInfo)
        //{
        //    if (id != shortUrlInfo.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _dbContext.Entry(shortUrlInfo).State = EntityState.Modified;

        //    try
        //    {
        //        await _dbContext.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ShortUrlInfoExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        ////DELETE: api/ShortUrlInfos/1
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<UrlInfo>> DeleteShortUrl(int id)
        //{
        //    if (_dbContext.UrlInfos == null)
        //    {
        //        return NotFound();
        //    }

        //    var shortUrlInfo = await _dbContext.UrlInfos.FindAsync(id);

        //    if (shortUrlInfo == null)
        //    {
        //        return NotFound();
        //    }

        //    _dbContext.UrlInfos.Remove(shortUrlInfo);
        //    await _dbContext.SaveChangesAsync();

        //    return NoContent();
        //}


        private bool ShortUrlInfoExists(long id)
        {
            return (_dbContext.UrlInfos?.Any(u => u.Id == id)).GetValueOrDefault();
        }

        private bool UrlExists(string url)
        {
            return (_dbContext.UrlInfos?.Any(o => o.Url == url)).GetValueOrDefault();
        }

        private bool ShortUrlExists(string shortUrl)
        {
            return (_dbContext.UrlInfos?.Any(o => o.ShortUrl == shortUrl)).GetValueOrDefault();
        }
    }
}
