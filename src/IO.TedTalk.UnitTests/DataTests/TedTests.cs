using System;
using System.Linq;
using Io.TedTalk.Core.Entities;
using Io.TedTalk.Tests;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace IO.TedTalk.UnitTests.DataTests;
public class TedTests : DbTestHelper
{
    [SetUp]
    public void EnsureDatabaseCreated()
    {
        DbContext.Database.EnsureCreated();
    }
    [Test]
    public void TableShouldGetCreated()
    {
        Assert.True(DbContext.Ted.Any());
    }

    [Test]
    public void CheckRequiredFields()
    {
        var newTed = new Ted();
        DbContext.Ted.Add(newTed);

        Assert.Throws<DbUpdateException>(() => DbContext.SaveChanges());
    }


    [Test]
    public void AddedTedShouldGetGeneratedId()
    {
        var newTed = DummyData.Sample;
        DbContext.Ted.Add(newTed);
        DbContext.SaveChanges();

        Assert.AreNotEqual(0, newTed.Id);
    }

    [Test]
    public void AddedTedShouldGetPersisted()
    {
        var newTed = DummyData.Sample2;
        DbContext.Ted.Add(newTed);
        DbContext.SaveChanges();

        Assert.AreEqual(newTed, DbContext.Ted.Find(newTed.Id));
    }

    [TearDown]
    public void RemoveDatabase()
    {
        DbContext.Database.EnsureDeleted();
    }
}
