using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Services;
public class RedisService(IConfiguration configuration)
{
    private readonly string _redisHost = configuration["Redis:Host"];
    private readonly string _redisPort = configuration["Redis:Port"];
    private ConnectionMultiplexer _redis;

    public void Connect()
    {
        var configString = $"{_redisHost}:{_redisPort}";
        _redis = ConnectionMultiplexer.Connect(configString);
    }

    public IDatabase GetDb(int db) => _redis.GetDatabase(db);
}