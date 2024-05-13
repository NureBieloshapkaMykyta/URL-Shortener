using Application.Helpers;
using Domain.Models;
using System.Linq.Expressions;

namespace Application.Abstractions;

public interface IUrlService : IRepository<Url>
{
    Task<Result<Url>> GetUrlByCode(string code);

    Task<bool> UrlExists(string url);

    Task<bool> PermissionToDelete(Guid userId, Guid urlId);
}
