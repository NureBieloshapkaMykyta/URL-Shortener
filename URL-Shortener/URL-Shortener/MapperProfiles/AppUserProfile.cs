using AutoMapper;
using Domain.Models;
using URL_Shortener.DTOs.Requests;

namespace URL_Shortener.MapperProfiles;

public class AppUserProfile : Profile
{
    public AppUserProfile()
    {
        CreateMap<RegisterAccountRequest, AppUser>().ForMember(mem => mem.PasswordHash, opt => opt.MapFrom(src => src.Password));
    }
}
