namespace Microsoft.Samples.Cosmos.MongoDB.Quickstart.Models.Settings;

public record Configuration
{
    public required AzureCosmosDB AzureCosmosDB { get; init; }
}

public record AzureCosmosDB
{
    public required string ConnectionString { get; init; }

    public required string DatabaseName { get; init; }

    public required string CollectionName { get; init; }
}