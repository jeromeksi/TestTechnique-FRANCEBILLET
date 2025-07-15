namespace StockBack.API.Articles.DTOs;
// Un DTO qui correspond à l'objet de base pour le retour d'API
public record ArticleResponse
{
    public int Id { get; init; } = 0;
    public string Reference { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public double PriceHT { get; init; } = 0.0f;
    public double PriceTTC { get; init; } = 0.0f;
    public bool CanTakeAway { get; init; }
    public TypeArticleDTOs Type { get; init; }
    public int Storage { get; init; }
}
