using StockBack.Application.Articles.Interfaces;
using StockBack.Application.Articles.Commands;
using StockBack.Domain.Articles.Entities;
using StockBack.Domain.Articles.Interfaces;
using StockBack.Application.Articles.Mapping;

namespace StockBack.Application.Articles.Services;
/*
Classe qui expose des méthodes qui appellent la couche Infrastructure. 
Dans le cas de ce test, c'est un peu trop d'abstraction mais c'est la bonne manière de faire pour de futur évolution
Cette Classe pourrait communiquer avec d'autre classe d'Infrastructure (Pannier, Stockage, User...)
*/
public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;

    public ArticleService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public Task<Article> CreateArticleAsync(CreateArticleCommands articleCommands)
    {
        if (articleCommands.Price < 0)
            throw new ArticlePriceInvalidException("ARTICLE_PRICE_NEGATIF",null);
        if (articleCommands.Storage < 0)
            throw new ArticleStorageInvalidException("ARTICLE_STORAGE_NEGATIF", null);

        try
        {
            return _articleRepository.CreateArticleAsync(articleCommands.ToDomain());
        }
        catch (Exception ex)
        {
            throw new ArticleAlreadyExistException(ex);
        }
    }

    public Task<bool> DeleteArticleAsync(string reference)
    {
        try 
        {
            return _articleRepository.DeleteArticleAsync(reference);
        }
        catch (Exception ex) 
        { 
            throw new ArticleUnknowException(ex);
        }
    }

    public Task<IEnumerable<Article>> GetArticleAllAsync()
    {
        return _articleRepository.GetArticleAllAsync();
    }

    public Task<Article> GetArticleAsync(string reference)
    {
        try
        {
            return _articleRepository.GetArticleAsync(reference);

        }
        catch (Exception ex)
        {
            throw new ArticleUnknowException(ex);
        }
    }

    public Task<IEnumerable<Article>> GetByPriceBetweenAsync(ArticlePriceCommands priceCommands)
    {
        return _articleRepository.GetArticleBetweenPriceAsync(priceCommands.PriceMin,priceCommands.PriceMax);
    }

    public Task<Article> UpdateStorage(string reference, int amountStorage)
    {
        try
        {
            return _articleRepository.UpdateStorageAsync(reference, amountStorage);
        }
        catch (Exception ex)
        {
            throw new ArticleUnknowException(ex);
        }
    }
}
