using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class AppUser : IdentityUser<Guid>
{
    public AppUserRole Role { get; set; }

    public virtual ICollection<Url> Urls { get; set; }
}
