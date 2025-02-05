using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace LusiUtilsLibrary.Backend.APIs_REST;

/// <summary>
/// Passing the file name for the correct configuration of apis
/// {
///   "httpconfig": {
///     "routes": [
///       {
///         "requestname": "requestName",
///         "urlname": "url"
///       }
///     ]
///   }
/// }
/// </summary>
public class REST_RequestService : IREST_RequestService
{
    #region private fields
    private readonly HttpClient _httpClient;
    private readonly dynamic _routesConfig;
    private readonly ILogger<REST_RequestService> _logger;
    #endregion

    #region constructors
    /// <summary>
    /// REST_RequestService constructor
    /// </summary>
    /// <param name="logger">Logger interface of type REST_RequestService</param>
    /// <param name="settingsPath">SettingsPath string for the communication settings file</param>
    public REST_RequestService(ILogger<REST_RequestService> logger, string settingsPath = "communicationsettings.json")
    {
        _logger = logger;
        _routesConfig = LoadRoutesConfig(settingsPath);
        _httpClient = new HttpClient();
    }
    #endregion

    #region public methods

    /// <summary>
    /// Execute an async REST Request basing on a JSON configuration file.
    /// </summary>
    /// <typeparam name="T">Data type attended from the request.</typeparam>
    /// <param name="requestName">Request name searched in <c>communicationsettings.json</c>(standard) or another file name</param>
    /// <param name="requestType">Request type (GET, POST, PUT, DELETE).</param>
    /// <param name="requestBody">Request body (if not null).</param>
    /// <param name="args">Optional arguments to ad to url</param>
    /// <returns>Request result deserialized as <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If request name is not in communication file.</exception>
    /// <exception cref="Exception">If http request fails.</exception>
    public async Task<T> ExecuteRequestAsync<T>(string requestName, RequestType requestType, object? requestBody, params string[] args)
    {
        return await ExecuteRequestAsync<T>(requestName, requestType, requestBody, null, null, args);
    }

    /// <summary>
    /// Execute an async REST Request basing on a JSON configuration file.
    /// </summary>
    /// <typeparam name="T">Data type attended from the request.</typeparam>
    /// <param name="requestName">Request name searched in <c>communicationsettings.json</c>(standard) or another file name</param>
    /// <param name="requestType">Request type (GET, POST, PUT, DELETE).</param>
    /// <param name="requestBody">Request body (if not null).</param>
    /// <param name="parameters">Parameters to add to url</param>
    /// <param name="args">Optional arguments to ad to url</param>
    /// <returns>Request result deserialized as <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If request name is not in communication file.</exception>
    /// <exception cref="Exception">If http request fails.</exception>
    public async Task<T> ExecuteRequestAsync<T>(string requestName, RequestType requestType, object? requestBody, Dictionary<string, string> parameters, params string[] args)
    {
        return await ExecuteRequestAsync<T>(requestName, requestType, requestBody, parameters, null, args);
    }

    /// <summary>
    /// Execute an async REST Request basing on a JSON configuration file.
    /// </summary>
    /// <typeparam name="T">Data type attended from the request.</typeparam>
    /// <param name="requestName">Request name searched in <c>communicationsettings.json</c>(standard) or another file name</param>
    /// <param name="requestType">Request type (GET, POST, PUT, DELETE).</param>
    /// <param name="requestBody">Request body (if not null).</param>
    /// <param name="timeout">Timeout for the request</param>
    /// <param name="args">Optional arguments to ad to url</param>
    /// <returns>Request result deserialized as <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If request name is not in communication file.</exception>
    /// <exception cref="Exception">If http request fails.</exception>
    public async Task<T> ExecuteRequestAsync<T>(string requestName, RequestType requestType, object? requestBody, double timeout, params string[] args)
    {
        return await ExecuteRequestAsync<T>(requestName, requestType, requestBody, null, timeout, args);
    }

    /// <summary>
    /// Execute an async REST Request basing on a JSON configuration file.
    /// </summary>
    /// <typeparam name="T">Data type attended from the request.</typeparam>
    /// <param name="requestName">Request name searched in <c>communicationsettings.json</c>(standard) or another file name</param>
    /// <param name="requestType">Request type (GET, POST, PUT, DELETE).</param>
    /// <param name="requestBody">Request body (if not null).</param>
    /// <param name="parameters">Parameters to add to url</param>
    /// <param name="timeout">Timeout for the request</param>
    /// <param name="args">Optional arguments to ad to url</param>
    /// <returns>Request result deserialized as <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If request name is not in communication file.</exception>
    /// <exception cref="Exception">If http request fails.</exception>
    public async Task<T> ExecuteRequestAsync<T>(string requestName, RequestType requestType, object? requestBody, Dictionary<string, string> parameters, double? timeout, params string[] args)
    {
        string url = GetBaseUrl(requestName);

        if (parameters != null)
        {
            foreach (var param in parameters)
                url = url.Replace($"{{{param.Key}}}", param.Value);
        }

        if (args != null && args.Length > 0)
            url = string.Format(url, args);

        return await SendRequestAsync<T>(url, requestType, requestBody, timeout);
    }

    #endregion

    #region private methods

    private async Task<T> SendRequestAsync<T>(string url, RequestType requestType, object? requestBody, double? timeout = null)
    {
        if (timeout.HasValue)
            _httpClient.Timeout = TimeSpan.FromSeconds(timeout.Value);

        HttpResponseMessage response = null;

        switch (requestType)
        {
            case RequestType.GET:
                {
                    response = await _httpClient.GetAsync(url);
                    break;
                }
            case RequestType.POST:
                {
                    response = await _httpClient.PostAsync(url, CreateHttpContent(requestBody));
                    break;
                }
            case RequestType.PUT:
                {
                    response = await _httpClient.PutAsync(url, CreateHttpContent(requestBody));
                    break;
                }
            case RequestType.DELETE:
                {
                    response = await _httpClient.DeleteAsync(url);
                    break;
                }
            default:
                {
                    throw new ArgumentOutOfRangeException(nameof(requestType), "Invalid request type");
                }
        };

        response.EnsureSuccessStatusCode();

        string responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseContent);
    }

    #endregion

    #region private helpers

    private static dynamic LoadRoutesConfig(string settingsPath)
    {
        if (!File.Exists(settingsPath))
            throw new FileNotFoundException($"Configuration file {settingsPath} not found.");
        return JsonSerializer.Deserialize<dynamic>(File.ReadAllText(settingsPath));
    }

    private static HttpContent? CreateHttpContent(object requestBody)
    {
        return requestBody != null
            ? new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json")
            : null;
    }

    private string GetBaseUrl(string requestName)
    {
        dynamic httpConfig = _routesConfig.GetProperty("httpconfig");
        dynamic routes = httpConfig.GetProperty("routes");

        foreach (dynamic r in routes.EnumerateArray())
        {
            if (r.GetProperty("requestname").GetString() == requestName)
            {
                return r.GetProperty("urlname").GetString();
            }
        }

        throw new ArgumentOutOfRangeException(nameof(requestName), $"RequestName {requestName} not found in route table");
    }

    #endregion
}