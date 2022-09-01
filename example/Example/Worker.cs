namespace Example;

public class Worker
{
    private readonly ILogger<Worker> _logger;
    private readonly NiceHash _niceHash;

    public Worker(ILogger<Worker> logger, NiceHash niceHash)
    {
        _logger = logger;
        _niceHash = niceHash;
    }

    internal async Task ExecuteAsync()
    {
        try
        {
            var endpoint = "/main/api/v2/mining/rigs2";
            var time = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();

            var rigs = await _niceHash.Get(endpoint, true, time);
            Console.WriteLine(rigs.Content);

            var rigsJson = JsonSerializer.Deserialize<Rigs>(rigs.Content);
            Console.WriteLine(rigsJson.MinerStatuses.Mining);
        }
        catch (Exception ex)
        {
            _logger.LogError("{ex}", ex);
        }
    }
}
