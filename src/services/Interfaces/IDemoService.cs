namespace Microsoft.Samples.Cosmos.MongoDB.Quickstart.Services.Interfaces;

public interface IDemoService
{
    Task RunAsync(Func<string, Task> writeOutputAync);

    string GetEndpoint();
}