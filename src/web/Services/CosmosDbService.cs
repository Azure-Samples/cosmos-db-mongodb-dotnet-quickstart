
using Cosmos.Samples.Table.Quickstart.Web.Models;
using MongoDB.Driver;

internal interface IMongoDBService
{
    Task RunDemoAsync(Func<string, Task> writeOutputAync);
}

internal sealed class MongoDBService : IMongoDBService
{
    private readonly MongoClient  client;

    public MongoDBService(MongoClient client)
    {
        this.client = client;
    }

    public async Task RunDemoAsync(Func<string, Task> writeOutputAync)
    {
        // <get_table>
       // Get reference to database and container
        // Database reference with creation if it does not already exist
        var db = client.GetDatabase("adventure");
        await writeOutputAync($"Get database:\t{db.DatabaseNamespace.DatabaseName}");
        // </get_table>

        // <create_object_add> 

        // Create new item using composite key constructor
        // Create new object and upsert (create or replace) to container
        var _products = db.GetCollection<Product>("products");

        _products.InsertOne(new Product(
            Guid.NewGuid().ToString(),
            "gear-surf-surfboards",
            "Yamba Surfboard", 
            12, 
            false
        ));
        // </create_object_add>            

        // <read_item> 
        // Read a single item from container
        var product = (await _products.FindAsync(p => p.Name.Contains("Yamba"))).FirstOrDefault();

        await writeOutputAync("Single product:");
        await writeOutputAync(product.Name);
        // </read_item>

        // <query_items> 
        // Read multiple items from container
        _products.InsertOne(new Product(
            Guid.NewGuid().ToString(),
            "gear-surf-surfboards",
            "Sand Surfboard",
            4,
            false
        ));

        var products = _products.AsQueryable().Where(p => p.Category == "gear-surf-surfboards");

        await writeOutputAync("Multiple products:");
        foreach (var prod in products)
        {
             await writeOutputAync($"{prod.Name}");
        }
        // </query_items>
    }
}
