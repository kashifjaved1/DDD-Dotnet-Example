using DDDExample.Domain.Entities;
using DDDExample.Domain.ValueObjects;

Console.WriteLine("--- DDD Example: Creating and Managing an Order ---");

// 1. Create a Value Object for the shipping address.
var initialAddress = new Address("123 Main St", "Anytown", "CA", "12345");

// 2. Create the Order aggregate. 'Order' is the Aggregate Root.
var order = Order.Create(initialAddress);
Console.WriteLine($"Order created with ID: {order.Id}");
Console.WriteLine($"Initial total price: {order.TotalPrice:C}");

// 3. Add items to the order through the Aggregate Root.
// This ensures all business rules inside the Order class are checked.
try
{
    Console.WriteLine("\nAdding items to the order...");
    order.AddOrderItem(Guid.NewGuid(), 2, 25.50m); // Add 2 items at $25.50 each
    order.AddOrderItem(Guid.NewGuid(), 1, 99.99m); // Add 1 item at $99.99

    Console.WriteLine("Items added successfully.");
    Console.WriteLine($"New total price: {order.TotalPrice:C}");
    Console.WriteLine($"Total line items: {order.OrderItems.Count}");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

// 4. Interact with the aggregate.
Console.WriteLine("\n--- Order Details ---");
Console.WriteLine($"Shipping to: {order.ShippingAddress.Street}, {order.ShippingAddress.City}");
foreach (var item in order.OrderItems)
{
    Console.WriteLine($"- Item: {item.ProductId}, Quantity: {item.Quantity}, Price: {item.Price:C}");
}

// 5. Change the shipping address.
var newAddress = new Address("456 Market St", "New City", "NY", "54321");
order.ChangeShippingAddress(newAddress);
Console.WriteLine($"New Shipping Address: {order.ShippingAddress.Street}");

Console.WriteLine("\n--- Example Complete ---");
