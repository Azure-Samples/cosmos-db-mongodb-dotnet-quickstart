using Microsoft.Extensions.Options;
using Microsoft.Samples.Cosmos.MongoDB.Quickstart.Services;
using Microsoft.Samples.Cosmos.MongoDB.Quickstart.Services.Interfaces;
using Microsoft.Samples.Cosmos.MongoDB.Quickstart.Web.Components;
using MongoDB.Driver;

using Settings = Microsoft.Samples.Cosmos.MongoDB.Quickstart.Models.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddOptions<Settings.Configuration>().Bind(builder.Configuration.GetSection(nameof(Settings.Configuration)));

builder.Services.AddSingleton<MongoClient>((serviceProvider) =>
{
    // <create_client>
    IOptions<Settings.Configuration> configurationOptions = serviceProvider.GetRequiredService<IOptions<Settings.Configuration>>();
    Settings.Configuration configuration = configurationOptions.Value;

    string connectionString = configuration.AzureCosmosDB.ConnectionString;
    MongoClient client = new(connectionString);
    // </create_client>

    return client;
});

builder.Services.AddTransient<IDemoService, DemoService>();

var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
