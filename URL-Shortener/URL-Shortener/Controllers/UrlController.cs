using Application.Abstractions;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using URL_Shortener.DTOs.Requests;
using URL_Shortener.DTOs.Responses;
using URL_Shortener.Extensions;

namespace URL_Shortener.Controllers;

[ApiController]
[Route("[controller]")]
public class UrlController : Controller
{
    private readonly IUrlService _urlService;

    private readonly IMapper _mapper;

    public UrlController(IUrlService urlService, IMapper mapper)
    {
        _urlService = urlService;
        _mapper = mapper;
    }

    [Authorize]
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

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var getResult = await _urlService.GetAllAsync(url => url.Id == id);
        if (!getResult.IsSuccessful)
        {
            return BadRequest(getResult.Message);
        }

        return Ok(_mapper.Map<DetailsUrlResponse>(getResult.Data.FirstOrDefault()));
    }

    [HttpGet("full/{code}")]
    public async Task<IActionResult> GetFullUrl([FromRoute] string code)
    {
        var getResult = await _urlService.GetUrlByShortered(code);
        if (!getResult.IsSuccessful)
        {
            return BadRequest(getResult.Message);
        }

        return Ok(getResult.Data.BaseUrl);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _urlService.GetAllAsync();
        if (!result.IsSuccessful)
        {
            return BadRequest(result.Message);
        }


        return Ok(result.Data.Select(_mapper.Map<DisplayUrlResponse>));
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        if (!(await _urlService.PermissionToDelete(User.GetIdFromPrincipal(), User.GetRoleFromPrincipal(), id)))
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
