using System.Reflection;
using Io.TedTalk.Core.Entities;
using Io.TedTalk.Data.Csv;
using IO.TedTalk.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Io.TedTalk.Data.EntityConfigurations;
public class TedEntityTypeConfiguration : IEntityTypeConfiguration<Ted>
{
   
    public void Configure(EntityTypeBuilder<Ted> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(400);
        builder.Property(x => x.Author).HasMaxLength(100);
        builder.Property(x => x.Link).HasMaxLength(2000);

        //author and title combination should be unique
        //I had to comment this because I found some exceptional data in data.csv!
        //we can talk about this
        //builder.HasIndex(x => new { x.Title, x.Author })
        //    .IsUnique();

        //may be it can be better :-?
        string assemblyPath = AppDomain.CurrentDomain.BaseDirectory;
        var filePath = Path.Combine(assemblyPath, AppConsts.Database.CsvFilePath);
        var seedData = CsvDataHelper.ReadCsv(filePath);

        
        if (seedData.Any())
        {
            builder.HasData(seedData);
        }
    }
}
