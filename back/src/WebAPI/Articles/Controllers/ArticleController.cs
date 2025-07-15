using Microsoft.AspNetCore.Mvc;
using StockBack.API.Articles.DTOs;
using StockBack.API.Articles.Mapping;
using StockBack.Application.Articles.Interfaces;

namespace StockBack.API.Articles.Controllers;

[Route("api/article")]
[ApiController]
public class ArticleController : ControllerBase
{
    private readonly IArticleService _articleService;
    public ArticleController(IArticleService articleService) 
    {
        _articleService = articleService;
    }

    /*
    TODO : Il faudrait ajouter une gestion de paginations si le nombre d'article devient trop important
    TODO : La mise en place d'une stratégie de cache peut-être intéréssante
     */
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _articleService.GetArticleAllAsync();

        var articles = ArticleMapping.ToDTOs(result);

        return Ok(articles);
    }
    /*
    TODO : La mise en place d'une stratégie de cache peut-être intéréssante
     */
    [HttpGet("{reference}")]
    public async Task<IActionResult> GetOneByReferenceAsync(string reference) 
    {
        var article = await _articleService.GetArticleAsync(reference);

        return Ok(article);
    }
    [HttpDelete("{reference}")]
    public async Task<IActionResult> DeleteAsync(string reference)
    {
        var result = await _articleService.DeleteArticleAsync(reference);

        return result ? Ok() : NoContent();
    }

    [HttpPost()]
    public async Task<IActionResult> CreateAsync([FromBody]ArticleCreateRequest articleCreateRequest)
    {
        var article = await _articleService.CreateArticleAsync(ArticleMapping.ToCommands(articleCreateRequest));
        
        return Ok(article.ToDTOs());
    }

    [HttpPost("{reference}/update-storage")]
    public async Task<IActionResult> AddStorageAsync(string reference, [FromBody] int amount)
    {
        var article = await _articleService.UpdateStorage(reference, amount);

        return Ok(article.ToDTOs());
    }

    [HttpPost("price")]
    public async Task<IActionResult> GetByPriceBetweenAsync([FromBody] ArticlePriceRequest articlePrice)
    {
        var result = await _articleService.GetByPriceBetweenAsync(articlePrice.ToCommands());
            
        var articles = ArticleMapping.ToDTOs(result);
        
        return Ok(articles);
    }

    //Exposition de l'enum pour le front si besoin  
    [HttpOptions("type-articles")]
    public IActionResult GetTypeArticles()
    {
        var result = Enum.GetValues(typeof(TypeArticleDTOs))
            .Cast<TypeArticleDTOs>()
            .Select(e => new
            {
                Id = (int)e,
                Name = e.ToString()
            });

        return Ok(result);
    }
}
