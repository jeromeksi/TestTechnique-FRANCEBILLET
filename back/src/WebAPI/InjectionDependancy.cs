using StockBack.Application.Articles.Interfaces;
using StockBack.Application.Articles.Services;
using StockBack.Domain.Articles.Interfaces;
using StockBack.Infrastructure.Articles.Repository;
using StockBack.Infrastructure.Persistence;

namespace StockBack.API;
/*
Classe static avec méthode d'extension pour ajouter toute les injections de dépendance que l'app a besoin
 */
public static class InjectionDependancy
{
    public static IServiceCollection AddDependancy(this IServiceCollection services)
    {
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>(); 
        return services;
    }
}
