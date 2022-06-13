using CsvHelper.Configuration;
using Io.TedTalk.Core.Entities;

namespace Io.TedTalk.Data.Csv;
internal class TedMapConfiguration : ClassMap<Ted>
{
    public TedMapConfiguration()
    {
        //Map(m => m.Id).Name(nameof(Ted.Id).ToLower());
        Map(m => m.Title).Name(nameof(Ted.Title).ToLower());
        Map(m => m.Author).Name(nameof(Ted.Author).ToLower());
        Map(m => m.Views).Name(nameof(Ted.Views).ToLower());
        Map(m => m.Likes).Name(nameof(Ted.Likes).ToLower());
        Map(m => m.Date).Name(nameof(Ted.Date).ToLower());
        Map(m => m.Link).Name(nameof(Ted.Link).ToLower());
    }

}
