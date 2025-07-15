namespace StockBack.API.Articles.DTOs;

/*
Recod
 */
public record ArticleCreateRequest 
{
    public TypeArticleDTOs Type { get; init; } = TypeArticleDTOs.NonConsomable; 
    public string Name { get; init; } = string.Empty;
    public float Price { get; init; } = 0.0f;
    public int? Storage { get; init; } = null;
    public bool? CanTakeAway { get; init; } = false;
}