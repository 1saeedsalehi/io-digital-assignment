using IO.TedTalk.Api.Framrwork.AspNetCore.Mvc.Results.Wrapping;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IO.TedTalk.Api.Framrwork.AspNetCore.Mvc.Results;

public class ResultFilter : IResultFilter
{
    private readonly IActionResultWrapperFactory _actionResultWrapperFactory;
    private readonly IWebHostEnvironment _env;

    public ResultFilter(IActionResultWrapperFactory actionResultWrapperFactory, IWebHostEnvironment env)
    {
        _actionResultWrapperFactory = actionResultWrapperFactory;
        _env = env;
    }

    public virtual void OnResultExecuting(ResultExecutingContext context)
    {

        _actionResultWrapperFactory.CreateFor(context, _env).Wrap(context);
    }

    public virtual void OnResultExecuted(ResultExecutedContext context)
    {
        // Nothing
    }
}
