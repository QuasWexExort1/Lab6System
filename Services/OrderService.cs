using Lab6System.Models;
using MongoDB.Driver;

namespace Lab6System.Services;

public class OrderService
{
    private readonly IMongoCollection<Order> _orders;

    public OrderService(IMongoClient client, string dbName)
    {
        var db = client.GetDatabase(dbName);
        _orders = db.GetCollection<Order>("Orders");
    }

    public async Task<List<Order>> GetAll()
    {
        return await _orders.Find(_ => true).ToListAsync();
    }

    public async Task Create(Order order)
    {
        await _orders.InsertOneAsync(order);
    }
}