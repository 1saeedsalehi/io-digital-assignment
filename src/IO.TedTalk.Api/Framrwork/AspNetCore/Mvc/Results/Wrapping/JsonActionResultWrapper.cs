using IO.TedTalk.Api.Framrwork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IO.TedTalk.Api.Framrwork.AspNetCore.Mvc.Results.Wrapping;

/// <summary>
/// TODO: Not tested JsonResult but it should work
/// </summary>
public class JsonActionResultWrapper : IActionResultWrapper
{
    public void Wrap(ResultExecutingContext actionResult)
    {
        if (actionResult.Result is not JsonResult jsonResult)
        {
            throw new ArgumentException($"{nameof(actionResult)} should be JsonResult!");
        }

        if (jsonResult.Value is not IOApiResponseBase)
        {
            var response = new IOApiResponse(jsonResult.Value);

            actionResult.Result = new ObjectResult(response)
            {
                StatusCode = jsonResult.StatusCode
            };
        }
    }
}
