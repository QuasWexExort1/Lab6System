namespace Lab6System.Models;

public class Order
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Customer { get; set; } = "";
    public string ProductCode { get; set; } = "";
    public string ProductName { get; set; } = "";
    public int Quantity { get; set; }
    public DateTime OrderDate { get; set; }
    public double TotalPrice { get; set; }
    public bool IsCompleted { get; set; }
}