using Microsoft.AspNetCore.Mvc.Filters;

namespace IO.TedTalk.Api.Framrwork.AspNetCore.Mvc.Results.Wrapping;

public interface IActionResultWrapper
{
    void Wrap(ResultExecutingContext actionResult);
}
