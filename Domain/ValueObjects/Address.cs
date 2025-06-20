namespace DDDExample.Domain.ValueObjects;

// Represents a Value Object: Defined by its attributes, not an ID. It's immutable.
public record Address(string Street, string City, string State, string ZipCode);
