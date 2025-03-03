using Backend.Domain.Models.Anime;
using MongoDB.Driver;

namespace Backend.Infrastructure.Repositories;

public class TitleRepository
{
    private readonly IMongoCollection<TitleDto> _titles;
    private const string CollectionName = "titles";
    public TitleRepository(IMongoClient client, string databaseName)
    {
        var database = client.GetDatabase(databaseName);
        var collections = database.ListCollections().ToList();
        var collectionExists = collections.Any(c => c["name"] == CollectionName);

        if (!collectionExists)
        {
            database.CreateCollection(CollectionName);
        }
        
        _titles = database.GetCollection<TitleDto>(CollectionName);
    }
    
    public async Task<TitleDto> GetRandomTitle(CancellationToken cancellationToken = default)
    {
        var randomTitle = await _titles.Aggregate()
            .Sample(1)
            .FirstOrDefaultAsync(cancellationToken);

        return randomTitle;
    }
    
    public async Task<List<TitleDto>> GetTitles(int limit, int offset, CancellationToken cancellationToken = default)
    {
        return await _titles
            .Find(Builders<TitleDto>.Filter.Empty)
            .Skip(offset)
            .Limit(limit)
            .ToListAsync(cancellationToken);
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

        return await _titles
            .Find(filter)
            .Sort(sort)
            .Skip(Skip)
            .Limit(Limit)
            .ToListAsync();
    }
    
    public async Task<TitleDto> GetTitleByCode(string code, CancellationToken cancellationToken = default)
    {
        return await _titles
            .Find(Builders<TitleDto>.Filter.Eq(account => account.Code, code))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<List<TitleDto>> GetLatestUpdates(int limit, int skip, CancellationToken cancellationToken = default)
    {
        return await _titles
            .Find(FilterDefinition<TitleDto>.Empty)
            .SortByDescending(t => t.Updated)
            .Skip(skip)
            .Limit(limit)
            .ToListAsync(cancellationToken);
    }

    public async Task BulkUpdate(List<TitleDto> titles, CancellationToken cancellationToken = default)
    {
        if (titles == null || titles.Count == 0)
        {
            throw new ArgumentNullException(nameof(titles), "Titles cannot be null or empty.");
        }

        var bulkOps = new List<WriteModel<TitleDto>>();

        foreach (var title in titles)
        {
            var filter = Builders<TitleDto>.Filter.Eq(t => t.Id, title.Id);
            var replaceModel = new ReplaceOneModel<TitleDto>(filter, title) { IsUpsert = true };

            bulkOps.Add(replaceModel);
        }

        if (bulkOps.Count > 0)
        {
            await _titles.BulkWriteAsync(bulkOps);
        }
    }
}