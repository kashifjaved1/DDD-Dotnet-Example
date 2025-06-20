namespace DDDExample.Domain.Entities;

// Represents an Entity: It has a unique identity (Id).
public class OrderItem
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    private OrderItem(Guid productId, int quantity, decimal price)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public static OrderItem Create(Guid productId, int quantity, decimal price)
    {
        // Business rule validation can happen here
        if (quantity <= 0) throw new ArgumentException("Quantity must be positive.", nameof(quantity));
        if (price <= 0) throw new ArgumentException("Price must be positive.", nameof(price));
        
        return new OrderItem(productId, quantity, price);
    }
}
