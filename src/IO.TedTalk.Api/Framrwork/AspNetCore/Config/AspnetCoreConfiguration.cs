using IO.TedTalk.Api.Framrwork.Models;

namespace IO.TedTalk.Api.Framrwork.AspNetCore.Config;

public class AspnetCoreConfiguration : IAspnetCoreConfiguration
{
    public WrapResultAttribute DefaultWrapResultAttribute { get; }

    public AspnetCoreConfiguration()
    {
        DefaultWrapResultAttribute = new WrapResultAttribute();
    }
}
