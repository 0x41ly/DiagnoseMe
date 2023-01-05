using System;
using System.Net;
namespace Core.Application.Common.Errors;


public interface IServiceException
{
    public HttpStatusCode statusCode {get; }
    public string ErrorMessage {get; }
}