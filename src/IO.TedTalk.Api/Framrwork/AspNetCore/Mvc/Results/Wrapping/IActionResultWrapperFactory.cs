using Microsoft.AspNetCore.Mvc.Filters;

namespace IO.TedTalk.Api.Framrwork.AspNetCore.Mvc.Results.Wrapping;

public interface IActionResultWrapperFactory
{
    IActionResultWrapper CreateFor(ResultExecutingContext actionResult, IWebHostEnvironment env);
}
