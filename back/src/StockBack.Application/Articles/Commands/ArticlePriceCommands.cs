namespace StockBack.Application.Articles.Commands;
public record ArticlePriceCommands
{
    public int PriceMax { get; init; } = 0;
    public int PriceMin { get; init; } = 0;
}
