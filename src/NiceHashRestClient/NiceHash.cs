namespace NiceHashRestClient;

public class NiceHash
{
    private readonly string UrlRoot;
    private readonly string OrgId;
    private readonly string ApiKey;
    private readonly string ApiSecret;

    public NiceHash(IOptions<NiceHashOptions> options)
    {
        UrlRoot = options.Value.UrlRoot;
        OrgId = options.Value.OrgId;
        ApiKey = options.Value.ApiKey;
        ApiSecret = options.Value.ApiSecret;
    }

    private static string HashBySegments(string key, string apiKey, string time, string nonce, string orgId, string method, string encodedPath, string query, string bodyStr)
    {
        List<string> segments = new()
        {
            apiKey,
            time,
            nonce,
            null,
            orgId,
            null,
            method,
            encodedPath ?? null,
            query ?? null
        };

        if (bodyStr != null && bodyStr.Length > 0)
        {
            segments.Add(bodyStr);
        }
        return CalcHMACSHA256Hash(JoinSegments(segments), key);
    }
    private static string GetPath(string url)
    {
        var arrSplit = url.Split('?');
        return arrSplit[0];
    }
    private static string GetQuery(string url)
    {
        var arrSplit = url.Split('?');

        if (arrSplit.Length == 1)
        {
            return null;
        }
        else
        {
            return arrSplit[1];
        }
    }

    private static string JoinSegments(List<string> segments)
    {
        var sb = new StringBuilder();
        bool first = true;
        foreach (var segment in segments)
        {
            if (!first)
            {
                sb.Append('\0');
            }
            else
            {
                first = false;
            }

            if (segment != null)
            {
                sb.Append(segment);
            }
        }
        return sb.ToString();
    }

    private static string CalcHMACSHA256Hash(string plaintext, string salt)
    {
        string result = "";
        var enc = Encoding.Default;
        byte[]
        baText2BeHashed = enc.GetBytes(plaintext),
        baSalt = enc.GetBytes(salt);
        HMACSHA256 hasher = new(baSalt);
        byte[] baHashedText = hasher.ComputeHash(baText2BeHashed);
        result = string.Join("", baHashedText.Select(b => b.ToString("x2")).ToArray());
        return result;
    }

    public async Task<RestResponse> Get(string endpoint)
    {
        return await Get(endpoint, false, null);
    }

    public async Task<RestResponse> Get(string endpoint, bool auth, string time)
    {
        var client = new RestClient(UrlRoot);
        var request = new RestRequest(endpoint);

        if (auth)
        {
            string nonce = Guid.NewGuid().ToString();
            string digest = HashBySegments(ApiSecret, ApiKey, time, nonce, OrgId, "GET", GetPath(endpoint), GetQuery(endpoint), null);

            request.AddHeader("X-Time", time);
            request.AddHeader("X-Nonce", nonce);
            request.AddHeader("X-Auth", ApiKey + ":" + digest);
            request.AddHeader("X-Organization-Id", OrgId);
        }

        var response = await client.ExecuteAsync(request, Method.Get);
        return response;
    }

    public async Task<RestResponse> Post(string endpoint, string payload, string time, bool requestId)
    {
        var client = new RestClient(UrlRoot);
        var request = new RestRequest(endpoint);
        request.AddHeader("Accept", "application/json");
        request.AddHeader("Content-type", "application/json");

        string nonce = Guid.NewGuid().ToString();
        string digest = HashBySegments(ApiSecret, ApiKey, time, nonce, OrgId, "POST", GetPath(endpoint), GetQuery(endpoint), payload);

        if (payload != null)
        {
            request.AddJsonBody(payload);
        }

        request.AddHeader("X-Time", time);
        request.AddHeader("X-Nonce", nonce);
        request.AddHeader("X-Auth", ApiKey + ":" + digest);
        request.AddHeader("X-Organization-Id", OrgId);

        if (requestId)
        {
            request.AddHeader("X-Request-Id", Guid.NewGuid().ToString());
        }

        var response = await client.ExecuteAsync(request, Method.Post);
        return response;
    }

    public async Task<RestResponse> Delete(string endpoint, string time, bool requestId)
    {
        var client = new RestClient(UrlRoot);
        var request = new RestRequest(endpoint);

        string nonce = Guid.NewGuid().ToString();
        string digest = HashBySegments(ApiSecret, ApiKey, time, nonce, OrgId, "DELETE", GetPath(endpoint), GetQuery(endpoint), null);

        request.AddHeader("X-Time", time);
        request.AddHeader("X-Nonce", nonce);
        request.AddHeader("X-Auth", ApiKey + ":" + digest);
        request.AddHeader("X-Organization-Id", OrgId);

        if (requestId)
        {
            request.AddHeader("X-Request-Id", Guid.NewGuid().ToString());
        }

        var response = await client.ExecuteAsync(request, Method.Delete);
        return response;
    }
}
