using Backend.Domain.Models.Anime;
using MongoDB.Driver;

namespace Backend.Infrastructure.Repositories;

public class AnimeRepository
{
    private readonly IMongoCollection<TitleDto> _collection;
    public AnimeRepository(IMongoClient client, string databaseName, string collectionName)
    {
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<TitleDto>(collectionName);
    }

    public async Task<TitleDto> GetTitleByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var filter = Builders<TitleDto>.Filter.Eq(account => account.Code, code);
        return await _collection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task Add(TitleDto title) =>
        await _collection.InsertOneAsync(title);

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