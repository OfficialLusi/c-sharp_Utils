namespace LusiUtilsLibrary.Backend.APIs_REST;

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
public interface IREST_RequestService
{
    // sync

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
    public T ExecuteRequestSync<T>(string requestName, RequestType requestType, object requestBody, params string[] args);

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
    public T ExecuteRequestSync<T>(string requestName, RequestType requestType, object requestBody, Dictionary<string, string> parameters, params string[] args);

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
    public T ExecuteRequestSync<T>(string requestName, RequestType requestType, object requestBody, double timeout, params string[] args);

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
    public T ExecuteRequestSync<T>(string requestName, RequestType requestType, object requestBody, Dictionary<string, string>? parameters, double? timeout, params string[] args);

    // async

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
    public Task<T> ExecuteRequestAsync<T>(string requestName, RequestType requestType, object requestBody, params string[] args);

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
    public Task<T> ExecuteRequestAsync<T>(string requestName, RequestType requestType, object requestBody, Dictionary<string, string> parameters, params string[] args);

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
    public Task<T> ExecuteRequestAsync<T>(string requestName, RequestType requestType, object requestBody, double timeout, params string[] args);

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
    public Task<T> ExecuteRequestAsync<T>(string requestName, RequestType requestType, object requestBody, Dictionary<string, string> parameters, double? timeout, params string[] args);
}