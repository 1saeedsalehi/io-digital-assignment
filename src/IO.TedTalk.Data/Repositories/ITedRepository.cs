using Io.TedTalk.Core.DTOs;
using Io.TedTalk.Core.Entities;

namespace Io.TedTalk.Services.Repositories;

/// <summary>
/// I'm not belive in making repository on top of another repository
/// because I am using ef core as ORM and DbContext is already is a combination of unit of work and repository pattern
/// but I made this to just show my abilities in using design patterns :)
/// I know that there are lots of cons/pros of using this pattern, so we can discuss about that 
/// </summary>
public interface ITedRepository
{
    Task<IEnumerable<Ted>> GetAll(GetTedInputDto input, CancellationToken cancellation = default);
    Task<Ted> GetById(int id, CancellationToken cancellationToken = default);
    ValueTask<int> Create(Ted entity, CancellationToken cancellationToken = default);
    Task Update(int id,Ted entity, CancellationToken cancellationToken = default);
    Task Delete(int id, CancellationToken cancellationToken = default);

}
