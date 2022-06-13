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

    public override string ToString()
    {
        //used for cache key
        //it can be impelemented in a better way
        return $"{nameof(GetTedInputDto)}-{Author}-{Title}-{Views}-{Likes}-{MaxResultCount}-{SkipCount}";
    }
}
