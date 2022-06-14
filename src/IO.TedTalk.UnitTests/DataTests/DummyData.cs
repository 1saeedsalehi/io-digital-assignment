using Io.TedTalk.Core.Entities;

namespace IO.TedTalk.UnitTests.DataTests;

public static class DummyData
{
    public static Ted Sample = new()
    {
        Title = "Dummy",
        Author = "Dummy",
        Date = "Dummy",
        Likes = "0",
        Link = "http://localhost",
        Views = "0"
    };

    public static Ted Sample2 = new()
    {
        Title = "Dummy",
        Author = "Dummy",
        Date = "Dummy",
        Likes = "0",
        Link = "http://localhost",
        Views = "0"
    };
}
