using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.Cosmos;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var secretClient = new SecretClient(new Uri(builder.Configuration["KEYVAULT_ENDPOINT"]), new DefaultAzureCredential());
var secretCosmos = await secretClient.GetSecretAsync("cosmosconnectionstring");

builder.Services.AddSingleton<MongoClient>((_) =>
{
    // </create_client>
    return new MongoClient(secretCosmos.Value.Value);
});

builder.Services.AddTransient<IMongoDBService, MongoDBService>();

var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
