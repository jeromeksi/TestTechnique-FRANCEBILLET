using StockBack.Application.Articles;

namespace StockBack.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    private static readonly Dictionary<Type, int> ExceptionStatusCodeMap = new() //Dictionaire pour chaque type d'exception
    {
        [typeof(ArticleUnknowException)] = StatusCodes.Status404NotFound,
        [typeof(ArticlePriceInvalidException)] = StatusCodes.Status400BadRequest,
        [typeof(ArticleAlreadyExistException)] = StatusCodes.Status409Conflict,
        [typeof(KeyNotFoundException)] = StatusCodes.Status404NotFound,
        [typeof(InvalidOperationException)] = StatusCodes.Status500InternalServerError,
        [typeof(ArticleStorageInvalidException)] = StatusCodes.Status400BadRequest,
    };

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            var statusCode = ResolveStatusCode(ex);

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new
            {
                error = ex.Message
            });
        }
    }

    private int ResolveStatusCode(Exception ex)
    {
        return ExceptionStatusCodeMap.TryGetValue(ex.GetType(), out var statusCode)
            ? statusCode
            : StatusCodes.Status500InternalServerError;
    }
}
