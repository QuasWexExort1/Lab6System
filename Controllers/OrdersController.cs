using Lab6System.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Lab6System.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly MongoService _db;

    public OrdersController(MongoService db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var data = await _db.Orders.Find(_ => true).ToListAsync();
        return Ok(data);
    }
}