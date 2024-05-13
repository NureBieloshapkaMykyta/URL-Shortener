using Application.Abstractions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using URL_Shortener.DTOs.Requests;
using URL_Shortener.Extensions;

namespace URL_Shortener.Controllers;

[ApiController]
[Route("[controller]")]
public class UrlController : Controller
{
    private readonly IUrlService _urlService;

    public UrlController(IUrlService urlService)
    {
        _urlService = urlService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUrlRequest request)
    {
        var model = new Url()
        {
            BaseUrl = request.BaseUrl,
            CreatorId = User.GetIdFromPrincipal()
        };

        var createResult = await _urlService.AddItemAsync(model);
        if (!createResult.IsSuccessful)
        {
            return BadRequest(createResult.Message);
        }

        return NoContent();
    }

    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetById([FromRoute] Guid id) 
    //{
    //    var getResult = await _urlService.GetAllAsync(url=>url.Id == id);
    //    if (!getResult.IsSuccessful) 
    //    {
    //        return BadRequest(getResult.Message);
    //    }

    //    return Ok(getResult.Data.FirstOrDefault());
    //}

    //[HttpGet]
    //public async Task<IActionResult> Get() 
    //{
    //    var result = await _urlService.GetAllAsync();
    //    if (!result.IsSuccessful) 
    //    {
    //        return BadRequest(result.Message);
    //    }

    //    return Ok(result.Data);
    //}

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        if (!(await _urlService.PermissionToDelete(User.GetIdFromPrincipal(), id)))
        {
            return Forbid("No permission");
        }

        var deleteResult = await _urlService.DeleteItemAsync(id);
        if (!deleteResult.IsSuccessful) 
        {
            return BadRequest(deleteResult.Message);
        }

        return NoContent();
    }
}
