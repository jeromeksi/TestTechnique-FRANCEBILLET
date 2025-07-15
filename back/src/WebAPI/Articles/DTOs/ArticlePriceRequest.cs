namespace StockBack.API.Articles.DTOs;
/*
 Record pour la request pour les recherches entre 2 prix.
 */
public record ArticlePriceRequest
{
    public int PriceMax { get; init; } = 0;
    public int PriceMin { get; init; } = 0;
}