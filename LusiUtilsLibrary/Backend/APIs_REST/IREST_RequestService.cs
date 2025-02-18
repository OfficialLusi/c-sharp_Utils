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
public interface IREST_RequestService
{  
    /// <summary>
    /// Execute an async REST Request basing on a JSON configuration file.
    /// </summary>
    /// <typeparam name="T">Data type attended from the request.</typeparam>
    /// <param name="requestName">Request name searched in <c>communicationsettings.json</c>(standard) or another file name</param>
    /// <param name="requestType">Request type (GET, POST, PUT, DELETE).</param>
    /// <param name="requestBody">Request body (if not null).</param>
    /// <returns>Request result deserialized as <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If request name is not in communication file.</exception>
    /// <exception cref="Exception">If http request fails.</exception>
    public Task<ApiResponse<T>> ExecuteRequestAsync<T>(string requestName, RequestType requestType, object? requestBody);

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
    public Task<ApiResponse<T>> ExecuteRequestAsync<T>(string requestName, RequestType requestType, object requestBody, params string[] args);

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
    public Task<ApiResponse<T>> ExecuteRequestAsync<T>(string requestName, RequestType requestType, object requestBody, Dictionary<string, string> parameters, params string[] args);

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
    public Task<ApiResponse<T>> ExecuteRequestAsync<T>(string requestName, RequestType requestType, object requestBody, double timeout, params string[] args);

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
    public Task<ApiResponse<T>> ExecuteRequestAsync<T>(string requestName, RequestType requestType, object requestBody, Dictionary<string, string> parameters, double? timeout, params string[] args);
}