using Microsoft.Extensions.Options;
using Microsoft.Samples.Cosmos.MongoDB.Quickstart.Models;
using Microsoft.Samples.Cosmos.MongoDB.Quickstart.Services.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using Settings = Microsoft.Samples.Cosmos.MongoDB.Quickstart.Models.Settings;

namespace Microsoft.Samples.Cosmos.MongoDB.Quickstart.Services;

public sealed class DemoService(
    MongoClient client,
    IOptions<Settings.Configuration> configurationOptions
) : IDemoService
{
    private readonly Settings.Configuration configuration = configurationOptions.Value;

    public string GetEndpoint() => $"{client.Settings.Server.Host}:{client.Settings.Server.Port}";

    public async Task RunAsync(Func<string, Task> writeOutputAsync)
    {
        IMongoDatabase database = client.GetDatabase(configuration.AzureCosmosDB.DatabaseName);

        await writeOutputAsync($"Get database:\t{database.DatabaseNamespace.DatabaseName}");

        IMongoCollection<Product> collection = database.GetCollection<Product>(configuration.AzureCosmosDB.CollectionName);

        await writeOutputAsync($"Get collection:\t{collection.CollectionNamespace.CollectionName}");

        {
            Product document = new(
                id: "aaaaaaaa-0000-1111-2222-bbbbbbbbbbbb",
                category: "gear-surf-surfboards",
                name: "Yamba Surfboard",
                quantity: 12,
                price: 850.00m,
                clearance: false
            );

            await collection.ReplaceOneAsync<Product>(
                d => d.id == document.id,
                document,
                new ReplaceOptions { IsUpsert = true }
            );

            await writeOutputAsync($"Upserted document:\t{document}");
        }

        {
            Product document = new(
                id: "bbbbbbbb-1111-2222-3333-cccccccccccc",
                category: "gear-surf-surfboards",
                name: "Kiama Classic Surfboard",
                quantity: 25,
                price: 790.00m,
                clearance: false
            );

            await collection.ReplaceOneAsync<Product>(
                d => d.id == document.id,
                document,
                new ReplaceOptions { IsUpsert = true }
            );

            await writeOutputAsync($"Upserted document:\t{document}");
        }

        {
            IAsyncCursor<Product> documents = await collection.FindAsync<Product>(
                d => d.id == "aaaaaaaa-0000-1111-2222-bbbbbbbbbbbb" && d.category == "gear-surf-surfboards"
            );

            Product? document = await documents.SingleOrDefaultAsync();

            if (document is not null)
            {
                await writeOutputAsync($"Read document id:\t{document.id}");
                await writeOutputAsync($"Read document:\t{document}");
            }
        }

        {
            IQueryable<Product> documents = collection.AsQueryable().Where(
                d => d.category == "gear-surf-surfboards"
            );

            await writeOutputAsync($"Ran query");

            foreach (Product document in await documents.ToListAsync())
            {
                await writeOutputAsync($"Found document:\t{document.name}\t[{document.id}]");

            }
        }
    }
}