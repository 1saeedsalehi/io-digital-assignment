using Io.TedTalk.Core.Entities;
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
        builder.HasIndex(x => new { x.Title, x.Author })
            .IsUnique();
    }
}
