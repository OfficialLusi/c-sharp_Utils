using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace LusiUtilsLibrary.APIs_REST;

/// <summary>
/// Passing the file name for the correct configuration of apis
/// {
///   "HTTPConfig": {
///     "routes": [
///       {
///         "RequestName": "requestName",
///         "URLName": "url"
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

    #region Sync

    /// <summary>
    /// Execute a sync REST Request basing on a JSON configuration file passing only args.
    /// </summary>
    /// <typeparam name="T">Data type attended from the request.</typeparam>
    /// <param name="requestName">Request name searched in <c>communicationsettings.json</c>(standard) or another file name</param>
    /// <param name="requestType">Request type (GET, POST, PUT, DELETE).</param>
    /// <param name="requestBody">Request body (if not null).</param>
    /// <param name="args">Optional arguments to add to url</param>
    /// <returns>Request result deserialized as <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If request name is not in communication file.</exception>
    /// <exception cref="Exception">If http request fails.</exception>
    public T ExecuteRequestSync<T>(string requestName, RequestType requestType, object requestBody, params string[] args)
    {
        return ExecuteRequestSync<T>(requestName, requestType, requestBody, null, null, args);
    }

    /// <summary>
    /// Execute a sync REST Request basing on a JSON configuration file passing parameters and args.
    /// </summary>
    /// <typeparam name="T">Data type attended from the request.</typeparam>
    /// <param name="requestName">Request name searched in <c>communicationsettings.json</c>(standard) or another file name</param>
    /// <param name="requestType">Request type (GET, POST, PUT, DELETE).</param>
    /// <param name="requestBody">Request body (if not null).</param>
    /// <param name="parameters">Parameters to add to the url</param>
    /// <param name="args">Optional arguments to add to url</param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public T ExecuteRequestSync<T>(string requestName, RequestType requestType, object requestBody, Dictionary<string, string> parameters, params string[] args)
    {
        return ExecuteRequestSync<T>(requestName, requestType, requestBody, parameters, null, args);
    }

    /// <summary>
    /// Execute a sync REST Request basing on a JSON configuration file passing timeout and args.
    /// </summary>
    /// <typeparam name="T">Data type attended from the request.</typeparam>
    /// <param name="requestName">Request name searched in <c>communicationsettings.json</c>(standard) or another file name</param>
    /// <param name="requestType">Request type (GET, POST, PUT, DELETE).</param>
    /// <param name="requestBody">Request body (if not null).</param>
    /// <param name="timeout">Timeout for the request</param>
    /// <param name="args">Optional arguments to add to url</param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public T ExecuteRequestSync<T>(string requestName, RequestType requestType, object requestBody, double timeout, params string[] args)
    {
        return ExecuteRequestSync<T>(requestName, requestType, requestBody, null, timeout, args);
    }

    /// <summary>
    /// Execute a sync REST Request basing on a JSON configuration file passing parameters, timeout and args.
    /// </summary>
    /// <typeparam name="T">Data type attended from the request.</typeparam>
    /// <param name="requestName">Request name searched in <c>communicationsettings.json</c>(standard) or another file name</param>
    /// <param name="requestType">Request type (GET, POST, PUT, DELETE).</param>
    /// <param name="requestBody">Request body (if not null).</param>
    /// <param name="parameters">Optional parameters to add to the url</param>
    /// <param name="timeout">Timeout for the request</param>
    /// <param name="args">Optional arguments to add to url</param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public T ExecuteRequestSync<T>(string requestName, RequestType requestType, object requestBody, Dictionary<string, string>? parameters, double? timeout, params string[] args)
    {
        string url = GetBaseUrl(requestName);

        if (parameters != null)
        {
            foreach (var param in parameters)
                url = url.Replace($"{{{param.Key}}}", param.Value);
        }

        if (args != null && args.Length > 0)
            url = string.Format(url, args);

        return SendRequestSync<T>(url, requestType, requestBody, timeout);
    }

    #endregion

    #region Async

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
    public async Task<T> ExecuteRequestAsync<T>(string requestName, RequestType requestType, object requestBody, params string[] args)
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
    public async Task<T> ExecuteRequestAsync<T>(string requestName, RequestType requestType, object requestBody, Dictionary<string, string> parameters, params string[] args)
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
    public async Task<T> ExecuteRequestAsync<T>(string requestName, RequestType requestType, object requestBody, double timeout, params string[] args)
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
    public async Task<T> ExecuteRequestAsync<T>(string requestName, RequestType requestType, object requestBody, Dictionary<string, string> parameters, double? timeout, params string[] args)
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

    #endregion

    #region private methods

    #region Sync

    private T SendRequestSync<T>(string url, RequestType requestType, object requestBody, double? timeout = null)
    {
        if (timeout.HasValue)
            _httpClient.Timeout = TimeSpan.FromSeconds(timeout.Value);

        HttpResponseMessage response = requestType switch
        {
            RequestType.GET => _httpClient.GetAsync(url).Result,
            RequestType.POST => _httpClient.PostAsync(url, CreateHttpContent(requestBody)).Result,
            RequestType.PUT => _httpClient.PutAsync(url, CreateHttpContent(requestBody)).Result,
            RequestType.DELETE => _httpClient.DeleteAsync(url).Result,
            _ => throw new ArgumentOutOfRangeException(nameof(requestType), "Invalid request type"),
        };

        response.EnsureSuccessStatusCode();

        string responseContent = response.Content.ReadAsStringAsync().Result;
        return JsonSerializer.Deserialize<T>(responseContent);
    }

    #endregion

    #region Async

    private async Task<T> SendRequestAsync<T>(string url, RequestType requestType, object requestBody, double? timeout = null)
    {
        if (timeout.HasValue)
            _httpClient.Timeout = TimeSpan.FromSeconds(timeout.Value);

        HttpResponseMessage response = requestType switch
        {
            RequestType.GET => await _httpClient.GetAsync(url),
            RequestType.POST => await _httpClient.PostAsync(url, CreateHttpContent(requestBody)),
            RequestType.PUT => await _httpClient.PutAsync(url, CreateHttpContent(requestBody)),
            RequestType.DELETE => await _httpClient.DeleteAsync(url),
            _ => throw new ArgumentOutOfRangeException(nameof(requestType), "Invalid request type"),
        };

        response.EnsureSuccessStatusCode();

        string responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseContent);
    }

    #endregion

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
        dynamic routes = _routesConfig["HttpConfig"]["routes"];
        dynamic route = null;

        foreach (var r in routes)
        {
            if (r["RequestName"].ToString() == requestName)
            {
                route = r;
                break;
            }
        }

        if (route == null)
            throw new ArgumentOutOfRangeException(nameof(requestName), $"RequestName {requestName} not found in route table");

        return route["UrlName"].ToString();
    }

    #endregion
}