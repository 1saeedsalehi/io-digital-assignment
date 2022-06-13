using Io.TedTalk.Core.DTOs;
using Io.TedTalk.Core.Entities;
using Io.TedTalk.Data.Extensions;
using IO.TedTalk.Core.Extensions;
using IO.TedTalk.Data;
using Microsoft.EntityFrameworkCore;

namespace Io.TedTalk.Services.Repositories.Implementations;

/// <summary>
/// please read my comments in interface :)
/// </summary>
public class TedRepository : ITedRepository
{
    private readonly IODbContext _dbContext;

    public TedRepository(IODbContext DbContext)
    {
        _dbContext = DbContext;
    }

    public async ValueTask<int> Create(Ted entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Ted.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task Delete(int idToBeDeleted, CancellationToken cancellationToken = default)
    {

        var itemToDelete = await _dbContext.Ted.FirstOrDefaultAsync(x => x.Id == idToBeDeleted);
        if (itemToDelete is null)
        {
            throw new IOException($"No Ted not found with given Id {idToBeDeleted}");
        }

        _dbContext.Ted.Remove(itemToDelete);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Ted>> GetAll(GetTedInputDto input, CancellationToken cancellation = default)
    {
        var teds = await _dbContext.Ted
            .AsNoTracking() // we don't need to track these entity
            .WhereIf(!input.Likes.IsNullOrWhiteSpace(), x => x.Likes == input.Likes)
            .WhereIf(!input.Views.IsNullOrWhiteSpace(), x => x.Views == input.Views)
            .WhereIf(!input.Title.IsNullOrWhiteSpace(), x => x.Title.Contains(input.Title))
            .WhereIf(!input.Author.IsNullOrWhiteSpace(), x => x.Likes.Contains(input.Likes))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount)
            .ToListAsync(cancellation);

        return teds;

    }

    public async Task<Ted> GetById(int id, CancellationToken cancellationToken = default)
    {
        var ted = await _dbContext.Ted.FirstOrDefaultAsync(x => x.Id == id);
        return ted;
    }

    public async Task Update(int id, Ted dto, CancellationToken cancellationToken = default)
    {
        var itemToUpdate = await _dbContext.Ted.FirstOrDefaultAsync(x => x.Id == dto.Id);
        if (itemToUpdate is null)
        {
            throw new IOException($"No Ted not found with given Id {itemToUpdate}");
        }

        //it can be implemented in a better way!
        itemToUpdate.Author = dto.Author;
        itemToUpdate.Date = dto.Date;
        itemToUpdate.Title = dto.Title;
        itemToUpdate.Likes = dto.Likes;
        itemToUpdate.Views = dto.Views;
        itemToUpdate.Link = dto.Link;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
