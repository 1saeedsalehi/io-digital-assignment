using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Io.TedTalk.Tests.DataTests;

public class IODbContextTests : DbTestHelper
{
    [Test]
    public async Task DatabaseIsAvailableAndCanBeConnectedTo()
    {
        Assert.True(await DbContext.Database.CanConnectAsync());
    }

    [Test]
    public async Task EnusreMigrationExecuted()
    {
        await DbContext.Database.MigrateAsync();
        //I know this is a magic number! but just to perform some tests
        Assert.AreEqual(5443, await DbContext.Ted.CountAsync());
    }


    [TearDown]
    public void RemoveDatabase()
    {
        DbContext.Database.EnsureDeleted();
    }
}
