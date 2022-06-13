namespace Io.TedTalk.Core.Entities;

public class Ted : BaseEntity
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Date { get; set; }
    //TODO: I found some strange data (maybe invalid) in csv file , so I've changed type to string! 
    public string Views { get; set; }
    //TODO: same as Views property :-?
    public string Likes { get; set; }
    public string Link { get; set; }

}
