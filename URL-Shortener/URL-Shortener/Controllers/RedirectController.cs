using Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace URL_Shortener.Controllers;

[ApiController]
[Route("test-surl")]
public class RedirectController : Controller
{
    private readonly IUrlService _urlService;

    public RedirectController(IUrlService urlService)
    {
        _urlService = urlService;
    }

    [HttpGet]
    [HttpPost]
    [Route("{id}")]
    public async Task<IActionResult> Redirect([FromRoute] string sourceId)
    {
        var getUrlResult = await _urlService.GetUrlByCode(sourceId);
        if (!getUrlResult.IsSuccessful) 
        {
            return BadRequest(getUrlResult.Message);
        }

        return await Redirect(getUrlResult.Data.BaseUrl);
    }
}
