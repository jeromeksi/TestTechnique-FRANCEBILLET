namespace StockBack.Infrastructure.Persistence;

/*
Classe avec l'ensemble des request SQL utile pour le reprepository.
A noter, comme j'utilise PostgreSQL, j'ai pu utilisé l'instruction RETURNING, qui n'est pas présent dans tout SGBDR
Il faudrait faire un fichier pour chaque SGBDR possible
EntityFramework simplifierai la tache.
*/
internal static class SQLRequestArticle
{
    public const string SELECT_ALL = @"
        SELECT 
            Id,
            Reference,
            Name,
            PriceHT,
            Storage,
            Type,
            CanTakeAway
        FROM Article;";

    public const string SELECT_BY_REF = @"
        SELECT 
            Id,
            Reference,
            Name,
            PriceHT,
            Storage,
            Type,
            CanTakeAway
        FROM Article;
        WHERE Reference = @Reference
        LIMIT 1;";

    public const string DELETE_BY_REF = @"
        DELETE FROM Article
        WHERE Reference = @Reference;";

    public const string CREATE_ARTICLE = @"
        INSERT INTO Article (Reference, Name, PriceHT, Storage, Type, CanTakeAway)
        VALUES (@Reference, @Name, @PriceHT, @Storage, @Type, @CanTakeAway)
        RETURNING Id;";

    public const string SELECT_BY_PRICE = @"
        SELECT 
            Id,
            Reference,
            Name,
            PriceHT,
            Storage,
            Type,
            CanTakeAway
        FROM Article
        WHERE PriceHT > @Min AND PriceHT < @Max;";
    public const string UPDATE_ARTICLE = @"
        UPDATE Article
        SET Storage = GREATEST(Storage + @Amount, 0)
        WHERE Reference = @Reference
        RETURNING Id, Reference, Name, PriceHT, Storage, Type, CanTakeAway;";

}
