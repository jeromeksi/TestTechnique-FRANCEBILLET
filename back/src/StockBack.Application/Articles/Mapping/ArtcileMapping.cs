using StockBack.Application.Articles.Commands;
using StockBack.Domain.Articles.Entities;

namespace StockBack.Application.Articles.Mapping;
/*
 Classe de mapping entre la couche Application et Infrastructure
 */
internal static class ArtcileMapping 
{
    public static Article ToDomain(this CreateArticleCommands createArticleCommands) =>
        new Article
        {
            PriceHT = createArticleCommands.Price ,
            Storage = (int)(createArticleCommands.Storage ?? 0),
            Type = createArticleCommands.Type,
            CanTakeAway = createArticleCommands.CanTakeAway,
            Name = createArticleCommands.Name,
        };
}
