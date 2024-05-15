using Application.Abstractions;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using URL_Shortener.DTOs.Requests;
using URL_Shortener.Extensions;

namespace URL_Shortener.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly IUserService _userService;

    private readonly IMapper _mapper;

    public AccountController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterAccountRequest request)
    {
        var registerResult = await _userService.RegisterUserAsync(_mapper.Map<AppUser>(request));

        return this.HandleResult(registerResult);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request) 
    {
        
        var loginResult = await _userService.AuthenticateUserAsync(request.Username, request.Password);

        return this.HandleResult(loginResult);
    }

    [Authorize]
    [HttpGet("Logout")]
    public async Task<IActionResult> Logout() 
    {
        var h = Request.Headers.Authorization;
        await _userService.SignOutAsync();

        return NoContent();
    }
}
