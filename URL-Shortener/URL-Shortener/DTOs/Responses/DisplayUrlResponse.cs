using Domain.Models;

namespace URL_Shortener.DTOs.Responses;

public class DisplayUrlResponse
{
    public string BaseUrl { get; set; }

    public string ShorteredUrl { get; set; }
}
