using Lab6System.Models;
using Lab6System.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Lab6System.Controllers;

[ApiController]
[Route("api/report")]
public class ReportController : ControllerBase
{
    private readonly MongoService _db;

    public ReportController(MongoService db)
    {
        _db = db;
    }

    [HttpGet("completed")]
    public async Task<IActionResult> GetCompleted(int year, int month)
    {
        if (year < 2000 || month < 1 || month > 12)
            return BadRequest("Invalid year or month");

        var start = new DateTime(year, month, 1);
        var end = start.AddMonths(1);

        var filter = Builders<Order>.Filter.And(
            Builders<Order>.Filter.Eq(x => x.IsCompleted, true),
            Builders<Order>.Filter.Gte(x => x.OrderDate, start),
            Builders<Order>.Filter.Lt(x => x.OrderDate, end)
        );

        var orders = await _db.Orders.Find(filter).ToListAsync();

        return Ok(new
        {
            count = orders.Count,
            totalQuantity = orders.Sum(x => x.Quantity),
            totalRevenue = orders.Sum(x => x.TotalPrice),
            orders
        });
    }
    [HttpGet("uncompleted")]
    public async Task<IActionResult> GetUncompleted()
    {
        var filter = Builders<Order>.Filter.Eq(x => x.IsCompleted, false);

        var orders = await _db.Orders.Find(filter).ToListAsync();

        return Ok(new
        {
            count = orders.Count,
            orders
        });
    }
}