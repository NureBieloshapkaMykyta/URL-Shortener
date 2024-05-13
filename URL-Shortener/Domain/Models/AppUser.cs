using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class AppUser : IdentityUser<Guid>
{
    public ICollection<Url> Urls { get; set; }

    public AppUserRole Role { get; set; }
}
