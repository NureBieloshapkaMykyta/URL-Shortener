using Application.Abstractions;
using Application.Helpers;
using Application.Implementations;
using Domain.Models;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace URL_Shortener.tests.ApplicationTests;

public class UrlServiceTests
{
    private readonly Mock<IRepository<Url>> _repositoryMock;

    private readonly UrlShortenerHelper _urlShortenerHelper;

    private readonly UrlService _urlService;

    public UrlServiceTests()
    {
        _repositoryMock = new Mock<IRepository<Url>>();
        _urlShortenerHelper = new UrlShortenerHelper();
        _urlService = new UrlService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetUrlByShortered_WhenUrlIsFound_ReturnsSuccessResult()
    {
        var expectedUrl = new Url { ShorteredUrl = "example.com/test-surl/abc", BaseUrl = "http://www.example.com" };
        _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Url, bool>>>()))
                      .Returns(()=>Task.FromResult(Result.Success<IEnumerable<Url>>(new List<Url> { expectedUrl })));

        var result = await _urlService.GetUrlByShortered("abc");

        Assert.True(result.IsSuccessful);
        Assert.Equal(expectedUrl.BaseUrl, result.Data.BaseUrl);
    }

    [Fact]
    public async Task GetUrlByShortered_WhenUrlDidNotFound_ReturnsError()
    {
        _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Url, bool>>>()))
                      .Returns(() => Task.FromResult(Result.Success<IEnumerable<Url>>(new List<Url>())));

        var result = await _urlService.GetUrlByShortered("abc");

        Assert.False(result.IsSuccessful);
        Assert.Equal("Failed to retrieve item", result.Message);
    }

    [Fact]
    public async Task GetUrlByShortered_WhenFailedToSearch_ReturnsError()
    {
        _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Url, bool>>>()))
                      .Returns(() => Task.FromResult(Result.Failure<IEnumerable<Url>>("Fail")));

        var result = await _urlService.GetUrlByShortered("abc");

        Assert.False(result.IsSuccessful);
        Assert.Equal("Failed to retrieve item", result.Message);
    }

    [Fact]
    public async Task UrlExists_WhenUrlIsFound_ReturnsSuccessResult()
    {
        var expectedUrl = new Url { BaseUrl = "http://www.example.com" };
        _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Url, bool>>>()))
                      .Returns(() => Task.FromResult(Result.Success<IEnumerable<Url>>(new List<Url> { expectedUrl })));

        var result = await _urlService.UrlExists(expectedUrl.BaseUrl);

        Assert.True(result);
    }

    [Fact]
    public async Task UrlExists_WhenUrlDidNotFound_ReturnsError()
    {
        var expectedUrl = new Url { BaseUrl = "http://www.example.com" };
        _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Url, bool>>>()))
                      .Returns(() => Task.FromResult(Result.Success<IEnumerable<Url>>(new List<Url> { })));

        var result = await _urlService.UrlExists("wrong-url");

        Assert.False(result);
    }

    [Fact]
    public async Task AddItemAsync_WhenItemAdded_ShouldCallRepositoryMethod()
    {
        var adding = new Url { BaseUrl = "http://www.example.com" };
        _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Url, bool>>>()))
                      .Returns(() => Task.FromResult(Result.Success<IEnumerable<Url>>(new List<Url> { })));
        _repositoryMock.Setup(repo => repo.AddItemAsync(It.IsAny<Url>()))
                      .Returns(() => Task.FromResult(Result.Success<bool>()));

        await _urlService.AddItemAsync(adding);

        _repositoryMock.Verify(x=>x.AddItemAsync(adding), Times.Once);
    }

    [Fact]
    public async Task AddItemAsync_WhenItemWithBaseUrlExists_ReturnsFailure()
    {
        var adding = new Url { BaseUrl = "http://www.example.com" };
        _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Url, bool>>>()))
                      .Returns(() => Task.FromResult(Result.Success<IEnumerable<Url>>(new List<Url> { adding })));
        _repositoryMock.Setup(repo => repo.AddItemAsync(It.IsAny<Url>()))
                      .Returns(() => Task.FromResult(Result.Success<bool>()));

        var result = await _urlService.AddItemAsync(adding);

        Assert.False(result.IsSuccessful);
        Assert.Equal("Such url already exists", result.Message);
    }

    [Fact]
    public async Task GetAllAsync_ShouldCallRepositoryMethod()
    {
        _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Url, bool>>>()))
                      .Returns(() => Task.FromResult(Result.Success<IEnumerable<Url>>(new List<Url> { })));

        var result = await _urlService.GetAllAsync(x=>x.BaseUrl.Length > 5);

        _repositoryMock.Verify(x => x.GetAllAsync(x => x.BaseUrl.Length > 5), Times.Once);
    }

    [Fact]
    public async Task DeleteItemAsync_ShouldCallRepositoryMethod()
    {
        var id = Guid.NewGuid();
        _repositoryMock.Setup(repo => repo.DeleteItemAsync(It.IsAny<Guid>()))
                      .Returns(() => Task.FromResult(Result.Success<bool>()));

        var result = await _urlService.DeleteItemAsync(id);

        _repositoryMock.Verify(x => x.DeleteItemAsync(id), Times.Once);
    }

    [Fact]
    public async Task UpdateItemAsync_WhenItemUpdated_ShouldCallRepositoryMethod()
    {
        var adding = new Url { BaseUrl = "http://www.example.com" };
        _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Url, bool>>>()))
                      .Returns(() => Task.FromResult(Result.Success<IEnumerable<Url>>(new List<Url> { })));
        _repositoryMock.Setup(repo => repo.UpdateItemAsync(It.IsAny<Url>()))
                      .Returns(() => Task.FromResult(Result.Success<bool>()));

        await _urlService.UpdateItemAsync(adding);

        _repositoryMock.Verify(x => x.UpdateItemAsync(adding), Times.Once);
    }

    [Fact]
    public async Task UpdateItemAsync_WhenItemWithBaseUrlExists_ReturnsFailure()
    {
        var adding = new Url { BaseUrl = "http://www.example.com" };
        _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Url, bool>>>()))
                      .Returns(() => Task.FromResult(Result.Success<IEnumerable<Url>>(new List<Url> { adding })));
        _repositoryMock.Setup(repo => repo.UpdateItemAsync(It.IsAny<Url>()))
                      .Returns(() => Task.FromResult(Result.Success<bool>()));

        var result = await _urlService.UpdateItemAsync(adding);

        Assert.False(result.IsSuccessful);
        Assert.Equal("Such url already exists", result.Message);
    }

    [Fact]
    public async Task PermissionToDelete_WhenUserIsAdmin_ReturnsTrue()
    {
        _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Url, bool>>>()))
                      .Returns(() => Task.FromResult(Result.Success<IEnumerable<Url>>(new List<Url> { })));

        var result = await _urlService.PermissionToDelete(Guid.NewGuid(), "Admin", Guid.NewGuid());

        Assert.True(result);
    }

    [Fact]
    public async Task PermissionToDelete_WhenUserIsCreator_ReturnsTrue()
    {
        var userId = Guid.NewGuid();
        var urlId = Guid.NewGuid();
        _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Url, bool>>>()))
                      .Returns(() => Task.FromResult(Result.Success<IEnumerable<Url>>(new List<Url> { new Url() { Id = urlId, CreatorId = userId } })));

        var result = await _urlService.PermissionToDelete(userId, "User", urlId);

        Assert.True(result);
    }

    [Fact]
    public async Task PermissionToDelete_WhenUserHasNotPermissions_ReturnsFalse()
    {
        _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Url, bool>>>()))
                      .Returns(() => Task.FromResult(Result.Success<IEnumerable<Url>>(null)));

        var result = await _urlService.PermissionToDelete(Guid.Empty, "User", Guid.Empty);

        Assert.False(result);
    }
}
