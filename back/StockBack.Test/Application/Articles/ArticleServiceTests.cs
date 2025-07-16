using Moq;
using StockBack.Application.Articles.Services;
using StockBack.Application.Articles.Commands;
using StockBack.Domain.Articles.Entities;
using StockBack.Domain.Articles.Interfaces;
using StockBack.Application.Articles;

namespace StockBack.Test.Application.Articles;
/*
 Class de test pour vérifier le comportement de la couche application (test des exceptions et de la creation correct)   
 */
public class ArticleServiceTests
{
    private readonly Mock<IArticleRepository> _repoMock;
    private readonly ArticleService _service;

    public ArticleServiceTests()
    {
        _repoMock = new Mock<IArticleRepository>();
        _service = new ArticleService(_repoMock.Object);
    }

    // Verification du refut de prix négatif
    [Fact]
    public async Task CreateArticleAsync_PriceNegative()
    {
        var cmd = new CreateArticleCommands { Price = -1, Storage = 1 };

        await Assert.ThrowsAsync<ArticlePriceInvalidException>(() =>
            _service.CreateArticleAsync(cmd));
    }

    // Verification du refut du stockage négatif
    [Fact]
    public async Task CreateArticleAsync_StorageNegative()
    {
        var cmd = new CreateArticleCommands { Price = 10, Storage = -5 };

        await Assert.ThrowsAsync<ArticleStorageInvalidException>(() =>
            _service.CreateArticleAsync(cmd));
    }

    // Verification du refut du nom déjà existant
    [Fact]
    public async Task CreateArticleAsync_NameAlreadyExist()
    {
        var cmd = new CreateArticleCommands { Price = 10, Storage = 1, Name = "Test" };

        _repoMock.Setup(r => r.CreateArticleAsync(It.IsAny<Article>()))
            .ThrowsAsync(new ArticleAlreadyExistException("REF_OR_NAME_ALREADY_EXIST", null));
        
        await Assert.ThrowsAsync<ArticleAlreadyExistException>(() =>
            _service.CreateArticleAsync(cmd));
    }
    
    // Verification de la creation d'un article correct
    [Fact]
    public async Task CreateArticleAsync_Valid()
    {
        var cmd = new CreateArticleCommands { Price = 10, Storage = 1, Name = "Test" };
        var expected = new Article()
        {
            Name = "Test",
            Type = TypeArticle.Consomable,
            CanTakeAway = false,
            PriceHT = 10, 
            Storage = 1,
        };

        _repoMock.Setup(r => r.CreateArticleAsync(It.IsAny<Article>()))
            .ReturnsAsync(expected);

        var result = await _service.CreateArticleAsync(cmd);

        _repoMock.Verify(r => r.CreateArticleAsync(It.IsAny<Article>()), Times.Once);
        Assert.Equal("Test", result.Name);
    }
}
