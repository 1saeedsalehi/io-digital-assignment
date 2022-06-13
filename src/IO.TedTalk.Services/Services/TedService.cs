using AutoMapper;
using Io.TedTalk.Core.DTOs;
using Io.TedTalk.Core.Entities;
using Io.TedTalk.Services.Repositories;

namespace Io.TedTalk.Services.Services;

public class TedService
{
    private readonly ITedRepository _repository;
    private readonly IMapper _mapper;

    public TedService(ITedRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public ValueTask<int> Create(CreateTedDto dto, CancellationToken cancellationToken = default)
    {
        var mappedEntity = _mapper.Map<Ted>(dto);
        return _repository.Create(mappedEntity, cancellationToken);
    }

    public Task Delete(int idToBeDeleted, CancellationToken cancellationToken = default)
    {
        return _repository.Delete(idToBeDeleted, cancellationToken);
    }

    public Task<IEnumerable<Ted>> GetAll(GetTedInputDto input, CancellationToken cancellation = default)
    {
        return _repository.GetAll(input, cancellation);
    }

    public Task<Ted> GetById(int id, CancellationToken cancellationToken = default)
    {
        return _repository.GetById(id, cancellationToken);
    }

    public Task Update(int idToUpdate, CreateTedDto dto, CancellationToken cancellationToken = default)
    {
        var mappedEntity = _mapper.Map<Ted>(dto);
        return _repository.Update(idToUpdate,mappedEntity, cancellationToken);
    }
}
