using IO.TedTalk.Data;
using Microsoft.EntityFrameworkCore;

namespace Io.TedTalk.Api;

public static class MigrationManager
{
    public static IHost MigrateDb(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<IODbContext>();
            db.Database.Migrate();
        }
        return host;
    }
}
