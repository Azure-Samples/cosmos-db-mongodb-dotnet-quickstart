using Azure;

namespace Cosmos.Samples.Table.Quickstart.Web.Models;

// <model>
// C# record type for items in the table
public record Product(
    string Id,
    string Category,
    string Name,
    int Quantity,
    bool Sale
);
// </model>
