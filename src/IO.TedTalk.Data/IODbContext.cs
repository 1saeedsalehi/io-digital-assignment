using System.Reflection;
using Io.TedTalk.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace IO.TedTalk.Data;

public partial class IODbContext : DbContext
{
    public DbSet<Ted> Ted { get; set; }

    public IODbContext()
    {
    }

    public IODbContext(DbContextOptions<IODbContext> options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}