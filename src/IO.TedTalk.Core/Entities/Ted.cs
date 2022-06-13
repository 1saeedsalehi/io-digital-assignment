namespace Io.TedTalk.Core.Entities;

public class Ted : BaseEntity
{
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime Date { get; set; }
    public long Views { get; set; }
    public long Likes { get; set; }
    public string Link { get; set; }

}
