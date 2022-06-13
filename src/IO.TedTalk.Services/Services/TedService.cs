using AutoMapper;
using Io.TedTalk.Core.DTOs;
using Io.TedTalk.Core.Entities;
using Io.TedTalk.Services.Repositories;
using IO.TedTalk.Core;
using IO.TedTalk.Core.Exceptions;
using Microsoft.Extensions.Caching.Memory;

namespace Io.TedTalk.Services.Services;

public class TedService
{
    private readonly ITedRepository _repository;
    private readonly IMapper _mapper;

    private readonly IMemoryCache _memoryCache;

    public TedService(ITedRepository repository,
        IMapper mapper,
        IMemoryCache memoryCache)
    {
        _repository = repository;
        _mapper = mapper;
        _memoryCache = memoryCache;
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

    public async Task<IEnumerable<Ted>> GetAll(GetTedInputDto input, CancellationToken cancellation = default)
    {
        IEnumerable<Ted> result;

        // If found in cache, return cached data
        var cacheKey = input.ToString();

        if (!_memoryCache.TryGetValue(cacheKey, out result))
        {
            //get and store in cache can be implemented with an extension method
            result = await _repository.GetAll(input, cancellation);
            _memoryCache.Set(cacheKey, result, AppConsts.Cache.CacheOptions);
        }

        return result;
    }

    public async Task<Ted> GetById(int id, CancellationToken cancellationToken = default)
    {
        var ted = await _repository.GetById(id, cancellationToken);
        if (ted is null)
        {
            throw new EntitytNotFoundException(typeof(Ted));
        }

        return ted;
    }

    public Task Update(int idToUpdate, CreateTedDto dto, CancellationToken cancellationToken = default)
    {
        //there is a known issue for caching
        //for example if we update and entity then try to get all
        //it will return an old value
        //we can handle this issue by changing the caching strategy

        var mappedEntity = _mapper.Map<Ted>(dto);
        return _repository.Update(idToUpdate, mappedEntity, cancellationToken);
    }
}
