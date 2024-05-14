using Application.Abstractions;
using Application.Helpers;
using Domain.Enums;
using Domain.Models;
using System.Linq.Expressions;

namespace Application.Implementations;

public class UrlService : IUrlService
{
    private readonly IRepository<Url> _repository;

    private readonly UrlShortenerHelper _shortenerHelper;

    public UrlService(IRepository<Url> repository)
    {
        _repository = repository;
        _shortenerHelper = new UrlShortenerHelper();
    }

    public async Task<Result<Url>> GetUrlByShortered(string shortered)
    {
        var getResult = await GetAllAsync(url=>url.ShorteredUrl == shortered);
        if (!getResult.IsSuccessful || !getResult.Data.Any())
        {
            return Result.Failure<Url>("Failed to retrieve item");
        }

        return Result.Success(getResult.Data.First());
    }

    public async Task<bool> UrlExists(string url)
    {
        var getResult = await GetAllAsync(u => u.BaseUrl == url);

        if (getResult.Data != null && getResult.Data.Any())
        {
            return true;
        }

        return false;
    }

    public async Task<Result<bool>> AddItemAsync(Url entity)
    {
        if (await UrlExists(entity.BaseUrl))
        {
            return Result.Failure<bool>("Such url already exists");
        }

        entity.ShorteredUrl = BaseUrlConstants.BaseUrl + _shortenerHelper.GenerateSurl();

        return await _repository.AddItemAsync(entity);
    }

    public async Task<Result<IEnumerable<Url>>> GetAllAsync(Expression<Func<Url, bool>>? predicate = null, int? pageNum = null, int? count = null)
    {
        return await _repository.GetAllAsync(predicate, pageNum, count);
    }

    public async Task<Result<bool>> DeleteItemAsync(Guid id)
    {
        return await _repository.DeleteItemAsync(id);
    }

    public async Task<Result<bool>> UpdateItemAsync(Url entity)
    {
        if (await UrlExists(entity.BaseUrl))
        {
            return Result.Failure<bool>("Such url already exists");
        }

        return await _repository.UpdateItemAsync(entity);
    }

    public async Task<bool> PermissionToDelete(Guid userId, string userRole, Guid urlId)
    {
        var url = await _repository.GetAllAsync(url=>url.Id == urlId);
        if ((userRole == AppUserRole.Admin.ToString()) || (url.Data!=null && userId == url.Data.First().CreatorId))
        {
            return true;
        }

        return false;
    }
}
