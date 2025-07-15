using StockBack.Application.Articles.Commands;
using StockBack.Domain.Articles.Entities;

namespace StockBack.Application.Articles.Interfaces;
public interface IArticleService
{
    Task<Article> GetArticleAsync(string reference);
    Task<IEnumerable<Article>> GetArticleAllAsync();
    Task<Article> CreateArticleAsync(CreateArticleCommands articleCommands);
    Task<bool> DeleteArticleAsync(string reference);
    Task<Article> UpdateStorage(string reference, int amount);
    Task<IEnumerable<Article>> GetByPriceBetweenAsync(ArticlePriceCommands priceCommands);
}
