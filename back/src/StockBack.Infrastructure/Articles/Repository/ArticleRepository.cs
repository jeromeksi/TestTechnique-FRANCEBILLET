using Dapper;
using Npgsql;
using StockBack.Domain.Articles.Entities;
using StockBack.Domain.Articles.Interfaces;
using StockBack.Infrastructure.Articles.Repository.SQL;
using StockBack.Infrastructure.Persistence;

namespace StockBack.Infrastructure.Articles.Repository;

/*
Représente le Design Pattern repository
Cette classe implémente les appels à la base de données avec le micro ORM Dapper.
Je trouve que Dapper est un orm simple et qui permet d'utilisé du SQL avec une forte sécurité sur la partie injection SQL.
*/
public class ArticleRepository : IArticleRepository
{
    private readonly IDbConnectionFactory _dbConnectionManager;
    public ArticleRepository(IDbConnectionFactory dbConnectionManager)
    {
        _dbConnectionManager = dbConnectionManager;
        _dbConnectionManager.CreateConnection();
    }
    public async Task<Article> CreateArticleAsync(Article createArticle)
    {
        using var connection = _dbConnectionManager.CreateConnection();

        try
        {
            createArticle.Id = await connection.ExecuteScalarAsync<int>(SQLRequestArticle.CREATE_ARTICLE, createArticle);
            return createArticle;
        }
        catch (Exception ex) //Il faudrait vérifier le type d'excepion
        {
            throw new InvalidOperationException($"REF_OR_NAME_ALREADY_EXISTE {createArticle.Name}");
        }
    }
    public async Task<bool> DeleteArticleAsync(string reference)
    {
        using var connection = _dbConnectionManager.CreateConnection();
   
        var rowsAffected = await connection.ExecuteAsync(SQLRequestArticle.DELETE_BY_REF, new { Reference = reference });

        if (rowsAffected == 0)
        {
            throw new InvalidOperationException($"REF_ALREADY_REMOVE : {reference}");
        }

        return true;
    }
    public async Task<IEnumerable<Article>> GetArticleAllAsync()
    {
        using var connexion = _dbConnectionManager.CreateConnection();

        return await connexion.QueryAsync<Article>(SQLRequestArticle.SELECT_ALL);
    }

    public async Task<Article> GetArticleAsync(string reference)
    {
        using var connection = _dbConnectionManager.CreateConnection();

        return await connection.QuerySingleOrDefaultAsync<Article>(SQLRequestArticle.SELECT_BY_REF, new { Reference = reference })
            ?? throw new ArgumentException($"REFERENCE_NOT_FOUND: {reference}");
    }
    public async Task<IEnumerable<Article>> GetArticleBetweenPriceAsync(float priceMin, float priceMax)
    {
        using var connection = _dbConnectionManager.CreateConnection();

        return await connection.QueryAsync<Article>(SQLRequestArticle.SELECT_BY_PRICE, new { Min = priceMin, Max = priceMax });
    }
    public async Task<Article> UpdateStorageAsync(string reference, int amountStorage)
    {
        using var connection = _dbConnectionManager.CreateConnection();

        var updated = await connection.QuerySingleOrDefaultAsync<Article>(SQLRequestArticle.UPDATE_ARTICLE, new
        {
            Reference = reference,
            Amount = amountStorage
        });

        if (updated == null)
        {
            throw new InvalidOperationException($"REF_DOESNT_EXIST :{reference}");
        }

        return updated;
    }
}
