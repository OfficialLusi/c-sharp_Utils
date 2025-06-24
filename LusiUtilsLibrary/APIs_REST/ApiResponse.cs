using System.Net;

namespace LusiUtilsLibrary.APIs_REST;

/// <summary>
/// Response type class
/// </summary>
/// <typeparam name="T"></typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// Data returned from the api call
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Status code of the api call
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }
}
