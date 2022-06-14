using System;
using IO.TedTalk.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Io.TedTalk.Tests;

public class DbTestHelper : IDisposable
{
    private const string InMemoryConnectionString = "DataSource=:memory:";
    private readonly SqliteConnection _connection;

    protected readonly IODbContext DbContext;

    protected DbTestHelper()
    {
        _connection = new SqliteConnection(InMemoryConnectionString);
        _connection.Open();
        var options = new DbContextOptionsBuilder<IODbContext>()
                .UseSqlite(_connection)
                .Options;
        DbContext = new IODbContext(options);
    }

    public void Dispose()
    {
        _connection.Close();
    }
}
