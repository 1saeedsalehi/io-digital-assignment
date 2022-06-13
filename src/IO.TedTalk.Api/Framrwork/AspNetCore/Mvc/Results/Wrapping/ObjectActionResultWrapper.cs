using IO.TedTalk.Api.Framrwork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IO.TedTalk.Api.Framrwork.AspNetCore.Mvc.Results.Wrapping;

public class ObjectActionResultWrapper : IActionResultWrapper
{
    private readonly IWebHostEnvironment _env;

    public ObjectActionResultWrapper(IWebHostEnvironment env)
    {
        _env = env;
    }

    public void Wrap(ResultExecutingContext actionResult)
    {
        if (actionResult.Result is not ObjectResult objectResult)
        {
            throw new ArgumentException($"{nameof(actionResult)} should be ObjectResult!");
        }

        if (objectResult.Value is not IOApiResponseBase)
        {
            var response = new IOApiResponse(objectResult.Value);

            actionResult.Result = new ObjectResult(response)
            {
                StatusCode = objectResult.StatusCode
            };
        }
    }
}
