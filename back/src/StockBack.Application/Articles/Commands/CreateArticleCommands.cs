using StockBack.Domain.Articles.Entities;

namespace StockBack.Application.Articles.Commands;
public record CreateArticleCommands
{
    public TypeArticle Type { get; init; } = TypeArticle.NonConsomable;
    public string Name { get; init; } = string.Empty;
    public float Price { get; init; } = 0.0f;
    public int? Storage { get; init; } = null;
    public bool CanTakeAway { get; init; } = false;
}
