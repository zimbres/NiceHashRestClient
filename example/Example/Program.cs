IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton<Worker>();
        var nicehashConfiguration = hostContext.Configuration.GetSection("NiceHashConfiguration").Get<NiceHashConfiguration>();
        services.AddSingleton<NiceHash>().Configure<NiceHashOptions>(config =>
        {
            config.UrlRoot = nicehashConfiguration.UrlRoot;
            config.OrgId = nicehashConfiguration.OrgId;
            config.ApiKey = nicehashConfiguration.ApiKey;
            config.ApiSecret = nicehashConfiguration.ApiSecret;
        });
    })
    .Build();

await host.Services.GetRequiredService<Worker>().ExecuteAsync();
