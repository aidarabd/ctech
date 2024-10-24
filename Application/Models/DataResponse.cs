using System.Net;

namespace Application.Models;

public class DataResponse
{
    public int StatusCode { get; set; } = (int)HttpStatusCode.OK;
    public string Message { get; set; }
}

public class DataResponse<T> : DataResponse
{
    public T Data { get; set; }

    public DataResponse(int statusCode, string message, T data)
    {
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }
}