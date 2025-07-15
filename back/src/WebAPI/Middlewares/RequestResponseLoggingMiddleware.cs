﻿namespace StockBack.API.Middlewares;
/*
C'est un middleware pour log les request entrante et leur sortie. 
Peut-être largement amélioré mais me permet de voir facilement ce qu'il se passe coté backend pour le developpement
 */
public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

    public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        context.Request.EnableBuffering();
        var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
        context.Request.Body.Position = 0;

        _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path} - Body: {requestBody}");
            
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        _logger.LogInformation($"Response: {context.Response.StatusCode} - Body: {responseText}");

        await responseBody.CopyToAsync(originalBodyStream);
    }
}
