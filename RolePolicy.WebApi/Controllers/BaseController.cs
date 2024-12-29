using AutoMapper;
using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace RolePolicy.WebApi.Controllers;

[ApiController]
[Route("error")]
public abstract class BaseController : ControllerBase
{
    protected IMediator Mediator => HttpContext.RequestServices.GetService<IMediator>()!;
    protected IMapper Mapper => HttpContext.RequestServices.GetService<IMapper>()!;

    protected ObjectResult ProblemResponse(Error error)
    {
        return error.Code switch
        {
            "400" => Problem(title: "Bad Request", detail: error.Message, statusCode: (int)HttpStatusCode.BadRequest),
            "401" => Problem(title: "Unauthorized", detail: error.Message, statusCode: (int)HttpStatusCode.Unauthorized),
            "403" => Problem(title: "Forbidden", detail: error.Message, statusCode: (int)HttpStatusCode.Forbidden),
            "404" => Problem(title: "Not Found", detail: error.Message, statusCode: (int)HttpStatusCode.NotFound),
            "500" => Problem(title: "Internal Server Error", detail: error.Message, statusCode: (int)HttpStatusCode.InternalServerError),
            "501" => Problem(title: "Not Implemented", detail: error.Message, statusCode: (int)HttpStatusCode.NotImplemented),
            "502" => Problem(title: "Bad Gateway", detail: error.Message, statusCode: (int)HttpStatusCode.BadGateway),
            "503" => Problem(title: "Service Unavailable", detail: error.Message, statusCode: (int)HttpStatusCode.ServiceUnavailable),
            _ => Problem(title: "Not Excepted Error", detail: error.Message, statusCode: (int)HttpStatusCode.InternalServerError)
        };
    }
}

