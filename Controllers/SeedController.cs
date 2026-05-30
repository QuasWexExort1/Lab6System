using Lab6System.Models;
using Lab6System.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Lab6System.Controllers;

[ApiController]
[Route("api/seed")]
public class SeedController : ControllerBase
{
    private readonly MongoService _db;

    public SeedController(MongoService db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Seed()
    {
        await _db.Products.DeleteManyAsync(Builders<Product>.Filter.Empty);
        await _db.Orders.DeleteManyAsync(Builders<Order>.Filter.Empty);

        var products = new List<Product>
        {
            new() { Code="P1", Name="Laptop", Price=1000 },
            new() { Code="P2", Name="Phone", Price=500 },
            new() { Code="P3", Name="Monitor", Price=300 },
            new() { Code="P4", Name="Keyboard", Price=50 },
            new() { Code="P5", Name="Mouse", Price=30 },
            new() { Code="P6", Name="Printer", Price=200 },
            new() { Code="P7", Name="Tablet", Price=400 }
        };

        await _db.Products.InsertManyAsync(products);

        var rand = new Random();
        var orders = new List<Order>();

        for (int i = 0; i < 15; i++)
        {
            var p = products[rand.Next(products.Count)];
            var qty = rand.Next(1, 10);

            orders.Add(new Order
            {
                Customer = $"Firm {rand.Next(1, 6)}",
                ProductCode = p.Code,
                ProductName = p.Name,
                Quantity = qty,
                OrderDate = DateTime.Now.AddDays(-rand.Next(0, 30)),
                TotalPrice = p.Price * qty,
                IsCompleted = rand.Next(0, 2) == 0
            });
        }

        await _db.Orders.InsertManyAsync(orders);

        return Ok(new
        {
            products = products.Count,
            orders = orders.Count
        });
    }
}