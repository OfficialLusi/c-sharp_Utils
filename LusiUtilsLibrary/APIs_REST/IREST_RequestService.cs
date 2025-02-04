using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LusiUtilsLibrary.APIs_REST.REST_RequestService;

namespace LusiUtilsLibrary.APIs_REST;

public interface IREST_RequestService
{
    // sync
    public T ExecuteRequestSync<T>(string requestName, RequestType requestType, object requestBody, params string[] args);
    public T ExecuteRequestSync<T>(string requestName, RequestType requestType, object requestBody, Dictionary<string, string> parameters, params string[] args);
    public T ExecuteRequestSync<T>(string requestName, RequestType requestType, object requestBody, double timeout, params string[] args);
    public T ExecuteRequestSync<T>(string requestName, RequestType requestType, object requestBody, Dictionary<string, string>? parameters, double? timeout, params string[] args);

    // async
    public Task<T> ExecuteRequestAsync<T>(string requestName, RequestType requestType, object requestBody, params string[] args);
    public Task<T> ExecuteRequestAsync<T>(string requestName, RequestType requestType, object requestBody, Dictionary<string, string> parameters, params string[] args);
    public Task<T> ExecuteRequestAsync<T>(string requestName, RequestType requestType, object requestBody, double timeout, params string[] args);
    public Task<T> ExecuteRequestAsync<T>(string requestName, RequestType requestType, object requestBody, Dictionary<string, string> parameters, double? timeout, params string[] args);
}
