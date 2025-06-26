using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Xaobec.MongoDbExtension;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbOptions>(configuration.GetSection("MongoDb"));
        services.AddTransient<MongoDbUrlBuilder>();
        services.AddSingleton<IMongoClient>(provider => 
            new MongoClient(provider.GetRequiredService<MongoDbUrlBuilder>().Build()));
        services.AddTransient<IMongoDatabase>(s =>
            s.GetRequiredService<IMongoClient>()
                .GetDatabase(configuration.GetValue<string>("MongoDb:DatabaseName", "defaultDatabase")));

        return services;
    }
}