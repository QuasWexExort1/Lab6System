using Lab6System.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Lab6System.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly MongoService _db;

    public ProductsController(MongoService db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var data = await _db.Products.Find(_ => true).ToListAsync();
        return Ok(data);
    }
}