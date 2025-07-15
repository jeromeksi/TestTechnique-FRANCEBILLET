using StockBack.Domain.Articles.Entities;

namespace StockBack.Domain.Articles.Interfaces;
/*
 Interface représentant la mise en place du Design Pattern de Repository
 */
public interface IArticleRepository
{
    Task<Article> GetArticleAsync(string reference); // Get Article by reference
    Task<IEnumerable<Article>> GetArticleAllAsync(); // Get all Article 
    Task<Article> CreateArticleAsync(Article article); // Create Article
    Task<bool> DeleteArticleAsync(string reference); // Delete Article
    Task<Article> UpdateStorageAsync(string reference, int amountStorage); //Update storage 
    Task<IEnumerable<Article>> GetArticleBetweenPriceAsync(float priceMin, float priceMax); //Get all article between price min/max
}