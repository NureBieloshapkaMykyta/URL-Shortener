using Application.Helpers;
using Domain.Models;
using System.Linq.Expressions;

namespace Application.Abstractions;

public interface IUrlService : IRepository<Url>
{
    Task<Result<Url>> GetUrlByShortered(string code);

    Task<bool> UrlExists(string url);

    Task<bool> PermissionToDelete(Guid userId, string userRole, Guid urlId);
}
