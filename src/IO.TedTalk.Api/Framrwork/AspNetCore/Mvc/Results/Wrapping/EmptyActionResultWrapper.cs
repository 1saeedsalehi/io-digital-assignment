using IO.TedTalk.Api.Framrwork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IO.TedTalk.Api.Framrwork.AspNetCore.Mvc.Results.Wrapping;

public class EmptyActionResultWrapper : IActionResultWrapper
{
    public void Wrap(ResultExecutingContext actionResult)
    {
        actionResult.Result = new ObjectResult(new IOApiResponse())
        {
            StatusCode = actionResult.HttpContext.Response.StatusCode
        };
    }
}
