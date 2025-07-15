using StockBack.API;
using StockBack.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables(); //Ajouts des variables d'environement, utile pour la configuration dans le docker compose ou le helm

builder.Services.AddControllers();

builder.Services.AddDependancy();

if (builder.Environment.IsDevelopment()) //Utilisation de Swagger pour tester API en developpement
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}
var app = builder.Build();

if (app.Environment.IsDevelopment()) //Utilisation de Swagger pour tester API en developpement
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
}); //J'ai déséactivé les cors 

app.UseMiddleware<RequestResponseLoggingMiddleware>(); //Ajout du middleware pour les logs de chaque request
app.UseMiddleware<ExceptionHandlingMiddleware>(); //Ajout du middleware pour les exceptions
app.MapControllers();

await app.RunAsync();
