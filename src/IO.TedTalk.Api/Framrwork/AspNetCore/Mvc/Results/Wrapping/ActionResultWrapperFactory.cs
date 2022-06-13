using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IO.TedTalk.Api.Framrwork.AspNetCore.Mvc.Results.Wrapping;

public class ActionResultWrapperFactory : IActionResultWrapperFactory
{
    public virtual IActionResultWrapper CreateFor(ResultExecutingContext actionResult, IWebHostEnvironment env)
    {
        if (actionResult.Result is ObjectResult)
        {
            return new ObjectActionResultWrapper(env);
        }
       

        return new NullActionResultWrapper();
    }
}
