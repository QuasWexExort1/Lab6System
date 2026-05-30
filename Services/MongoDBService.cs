using Lab6System.Models;
using MongoDB.Driver;

namespace Lab6System.Services;

public class MongoService
{
    private readonly IMongoDatabase _db;

    public IMongoCollection<Product> Products { get; }
    public IMongoCollection<Order> Orders { get; }

    public MongoService(IMongoClient client)
    {
        _db = client.GetDatabase("Lab6System");

        Products = _db.GetCollection<Product>("Products");
        Orders = _db.GetCollection<Order>("Orders");
    }
}