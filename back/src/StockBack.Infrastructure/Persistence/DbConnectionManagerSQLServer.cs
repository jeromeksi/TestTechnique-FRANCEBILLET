using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Npgsql;
using MySqlConnector;

namespace StockBack.Infrastructure.Persistence;

/*
Gestion de la connexion à la base de données.
J'ai fait la mise en place du Desing pattern Factory pour générer le bon type de connexion en fonction du driver
Les valeurs DatabaseProvider et StockDBConnection sont envoyé par la conf avec l'ajout des variable d'env ou par appsettings
*/
public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
public enum DatabaseProvider
{
    SqlServer,
    PostgreSQL,
    MySQL
}
public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly IConfiguration _configuration;
    private readonly DatabaseProvider _provider;

    public DbConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
        var providerStr = configuration["DBProvider"];
        if (!Enum.TryParse(providerStr, out _provider))
            throw new ArgumentException($"Invalid DBProvider: {providerStr}");
    }

    public IDbConnection CreateConnection()
    {
        var connectionString = _configuration["StockDBConnection"]
            ?? throw new ArgumentNullException("StockDBConnection");

        return _provider switch
        {
            DatabaseProvider.SqlServer => new SqlConnection(connectionString),
            DatabaseProvider.PostgreSQL => new NpgsqlConnection(connectionString),
            DatabaseProvider.MySQL => new MySqlConnection(connectionString),
            _ => throw new NotSupportedException($"Provider unknow : {_provider}") 
        };
    }
}
