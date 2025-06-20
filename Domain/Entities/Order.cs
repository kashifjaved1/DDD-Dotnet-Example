using DDDExample.Domain.ValueObjects;

namespace DDDExample.Domain.Entities;

// Represents the Aggregate Root. It's an entity that is the main entry point 
// for modifications to the 'Order' aggregate.
public class Order
{
    public Guid Id { get; private set; }
    public Address ShippingAddress { get; private set; }
    public decimal TotalPrice => _orderItems.Sum(item => item.Price * item.Quantity);

    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    private Order(Address shippingAddress)
    {
        Id = Guid.NewGuid();
        ShippingAddress = shippingAddress;
    }

    public static Order Create(Address shippingAddress)
    {
        // Validation for the aggregate can go here
        ArgumentNullException.ThrowIfNull(shippingAddress);
        return new Order(shippingAddress);
    }

    // This method enforces the aggregate's invariants (business rules).
    public void AddOrderItem(Guid productId, int quantity, decimal price)
    {
        // Rule: An order cannot have more than 10 unique items.
        if (_orderItems.Count >= 10)
        {
            throw new InvalidOperationException("An order cannot have more than 10 line items.");
        }

        var orderItem = OrderItem.Create(productId, quantity, price);
        _orderItems.Add(orderItem);

        // Other logic could happen here, like publishing a domain event.
    }

    public void ChangeShippingAddress(Address newAddress)
    {
        ArgumentNullException.ThrowIfNull(newAddress);
        ShippingAddress = newAddress;
        Console.WriteLine("Shipping address updated.");
    }
}
