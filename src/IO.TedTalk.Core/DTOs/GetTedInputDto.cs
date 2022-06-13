namespace Io.TedTalk.Core.DTOs;
public class GetTedInputDto   
{
    public string Author { get; set; }
    public string Title { get; set; }
    public string Views { get; set; }
    public string Likes { get; set; }

    public int MaxResultCount { get; set; }
    public int SkipCount { get; set; }

    public GetTedInputDto()
    {
        MaxResultCount = 10;
        SkipCount = 0;
    }
}
