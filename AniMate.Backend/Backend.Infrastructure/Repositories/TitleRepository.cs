using Backend.Domain.Models.Anime;
using MongoDB.Driver;

namespace Backend.Infrastructure.Repositories;

public class TitleRepository
{
    private readonly IMongoCollection<TitleDto> _collection;
    public TitleRepository(IMongoClient client, string databaseName, string collectionName)
    {
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<TitleDto>(collectionName);
    }
    
    public async Task<TitleDto> GetRandomTitle(CancellationToken ctx = default)
    {
        var randomTitle = await _collection.Aggregate()
            .Sample(1)
            .FirstOrDefaultAsync(ctx);

        return randomTitle;
    }
    
    public async Task<List<TitleDto>> GetTitles(int limit, int offset, CancellationToken ctx = default)
    {
        return await _collection
            .Find(Builders<TitleDto>.Filter.Empty)
            .Skip(offset)
            .Limit(limit)
            .ToListAsync(ctx);
    }
    
    public async Task<List<TitleDto>> SearchTitles(
        int Skip,
        List<string>? genres,
        string OrderBy,
        bool SortDirection,
        int Limit)
    {
        var filterBuilder = Builders<TitleDto>.Filter;
        var filter = filterBuilder.Empty;
        
        if (genres != null)
        {
            filter &= filterBuilder.ElemMatch(t => t.Genres, genre => genres.Contains(genre));
        }
        
        var sortBuilder = Builders<TitleDto>.Sort;
        var sortField = OrderBy ?? "Title";

        var sort = SortDirection ? sortBuilder.Ascending(sortField) : sortBuilder.Descending(sortField);

        return await _collection
            .Find(filter)
            .Sort(sort)
            .Skip(Skip)
            .Limit(Limit)
            .ToListAsync();
    }
    
    public async Task<TitleDto> GetTitleByCode(string code, CancellationToken ctx = default)
    {
        return await _collection
            .Find(Builders<TitleDto>.Filter.Eq(account => account.Code, code))
            .SingleOrDefaultAsync(ctx);
    }

    public async Task AddMany(List<TitleDto> titles)
    {
        if (titles == null)
        {
            throw new ArgumentNullException(nameof(titles), "Titles cannot be null.");
        }

        foreach (var title in titles)
        {
            var filter = Builders<TitleDto>.Filter.Eq(t => t.Id, title.Id);
            
            await _collection.ReplaceOneAsync(filter, title, new ReplaceOptions { IsUpsert = true });
        }
    }
}