namespace StockBack.Domain.Articles.Entities;
/*
Classe du Domaine qui représente les objets Articles dans l'applications.
*/
public class Article
{
    public int Id { get; set; } 
    public string Reference { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public double PriceHT { get; set; }
    public int Storage { get; set; }
    public TypeArticle Type { get; set; }
    public bool CanTakeAway { get; set; }
    public double PriceTTC => (float)Math.Round(PriceHT * (1 + TVA), 2);

    public double TVA { get
        {
            return Type == TypeArticle.Consomable && CanTakeAway ? 
                PriceHT * TaxeRate.TAKEAWAY_TAXE : 
                PriceHT * TaxeRate.NONE_TAKEAWAY_TAXE;
        }
    }
}
public static class TaxeRate
{
    public const float NONE_TAKEAWAY_TAXE = 1.20f;    
    public const float TAKEAWAY_TAXE = 1.055f;    
}
public enum TypeArticle
{
    Consomable = 1,
    NonConsomable = 2
}