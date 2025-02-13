using System.Net;

namespace LusiUtilsLibrary.Backend.APIs_REST;

public class ApiResponse<T>
{
    public T Data { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}
