using AutoMapper;
using Domain.Models;
using URL_Shortener.DTOs.Responses;

namespace URL_Shortener.MapperProfiles;

public class UrlProfile : Profile
{
    public UrlProfile()
    {
        CreateMap<Url, DisplayUrlResponse>();
        CreateMap<Url, DetailsUrlResponse>();
    }
}
