namespace Microsoft.Samples.Cosmos.MongoDB.Quickstart.Models;

public sealed record Product(
    string id,
    string category,
    string name,
    int quantity,
    decimal price,
    bool clearance
);