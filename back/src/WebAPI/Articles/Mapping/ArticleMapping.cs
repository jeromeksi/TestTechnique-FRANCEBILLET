using StockBack.API.Articles.DTOs;
using StockBack.Application.Articles.Commands;
using StockBack.Domain.Articles.Entities;

namespace StockBack.API.Articles.Mapping;

/*
J'ai utilisé deux manières de faire pour le même type de besoin (spéfiquement pour le test) 
ne sachant les conventions de FranceBillet. Dans un developpement en production réel j'utiliserai la manière courante de la société

Par convention j'utilise des Expression-bodied que je préfère 
mais une fonction classique aussi bien, ca dépendant des conventions de l'équipe de dev.
*/
internal static class ArticleMapping
{
    /*
    Méthode static de transformation 
    */
    public static CreateArticleCommands ToCommands(ArticleCreateRequest articleCreateRequest) => 
        new CreateArticleCommands()
        {
            Name = articleCreateRequest.Name,
            Price = articleCreateRequest.Price,
            CanTakeAway = articleCreateRequest.CanTakeAway ?? false && articleCreateRequest.Type == TypeArticleDTOs.Consomable,
            Type = articleCreateRequest.Type switch
            {
                TypeArticleDTOs.Consomable => TypeArticle.Consomable,
                TypeArticleDTOs.NonConsomable => TypeArticle.NonConsomable,
                _ => TypeArticle.NonConsomable
            }, // Ce switch fonctionne tant qu'on reste avec un minimun de cas sinon préférable de faire une fonction dédié
            Storage = articleCreateRequest.Storage
        };
   
    /*
    Méthode d'extension pour Article et le transformer en objet DTO d'API
    */
    public static ArticleResponse ToDTOs(this Article article) => new ArticleResponse
    {
        Id = article.Id,
        Name = article.Name,
        PriceHT = article.PriceHT,
        PriceTTC = article.PriceTTC,
        Reference = article.Reference,
        CanTakeAway = article.CanTakeAway,
        Type = article.Type switch
        {
            TypeArticle.Consomable=> TypeArticleDTOs.Consomable,
            TypeArticle.NonConsomable => TypeArticleDTOs.NonConsomable,
            _ => TypeArticleDTOs.NonConsomable
        }, // Ce switch fonctionne tant qu'on reste avec un minimun de cas sinon préférable de faire une fonction dédié
        Storage = article.Storage
    };


    /*
    Méthode static de transformation 
    */
    internal static IEnumerable<ArticleResponse> ToDTOs(IEnumerable<Article> result) =>
        result.AsParallel().Select(article => article.ToDTOs());

    internal static ArticlePriceCommands ToCommands(this ArticlePriceRequest articlePriceRequest)
        => new ArticlePriceCommands()
        {
            PriceMax = articlePriceRequest.PriceMax,
            PriceMin = articlePriceRequest.PriceMin,
        };
}
