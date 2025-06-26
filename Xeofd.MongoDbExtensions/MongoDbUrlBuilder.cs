using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Xaobec.MongoDbExtension;

public class MongoDbUrlBuilder
{
    private readonly IOptions<MongoDbOptions> _options;

    public MongoDbUrlBuilder(IOptions<MongoDbOptions> options)
    {
        _options = options;
    }

    public MongoClientSettings Build()
    {
        var buildFormsDbMongoUri = new MongoUrlBuilder(_options.Value.ConnectionString);

        if (_options.Value.Password is { Length: > 0 } password)
        {
            buildFormsDbMongoUri.Password = password;
        }

        var settings = MongoClientSettings.FromUrl(buildFormsDbMongoUri.ToMongoUrl());
        
        return settings;
    }
}