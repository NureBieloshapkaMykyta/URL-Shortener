using Domain.Enums;

namespace URL_Shortener.DTOs.Responses;

public class DetailsUserResponse
{
    public string Username { get; set; }

    public AppUserRole Role { get; set; }
}
