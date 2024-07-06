using StackExchange.Redis;

namespace RedisExampleApp.Cache;

public class RedisService(string url)
{
    private readonly ConnectionMultiplexer _connectionMultiplexer = ConnectionMultiplexer.Connect(url);

    public IDatabase GetDatabase(int dbIndex) => _connectionMultiplexer.GetDatabase(dbIndex);

    public IServer GetServer() => _connectionMultiplexer.GetServer(_connectionMultiplexer.GetEndPoints()[0]);

    public void Dispose() => _connectionMultiplexer.Dispose();

    public void FlushDatabase() => _connectionMultiplexer.GetServer(_connectionMultiplexer.GetEndPoints()[0]).FlushDatabase();

    public void FlushAllDatabases() => _connectionMultiplexer.GetServer(_connectionMultiplexer.GetEndPoints()[0]).FlushAllDatabases();
}
