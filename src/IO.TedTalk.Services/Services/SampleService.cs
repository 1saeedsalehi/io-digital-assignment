using AutoMapper;

namespace IO.TedTalk.Services.Services;
public class SampleService
{
    //injection
    private readonly IMapper _mapper;

    public SampleService(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Sample method!
    /// </summary>
    /// <returns></returns>
    public string Ping(string input)
    {
        return input.Equals("ping", StringComparison.InvariantCultureIgnoreCase)
            ? "pong!"
            : throw new Core.Exceptions.IOException("invalid parameter!");

    }
}
