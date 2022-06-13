using Microsoft.AspNetCore.Mvc.Filters;

namespace IO.TedTalk.Api.Framrwork.AspNetCore.Mvc.Results.Wrapping;

public class NullActionResultWrapper : IActionResultWrapper
{
    public void Wrap(ResultExecutingContext actionResult)
    {
        // Nothing as it's name suggests
    }
}
