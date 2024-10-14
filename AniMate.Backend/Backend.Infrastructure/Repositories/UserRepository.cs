using Backend.Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Backend.Infrastructure.Repositories;

public class UserRepository : IMongoRepository<User>
{
    private readonly IMongoCollection<User> _collection;
    public UserRepository(
        IMongoClient client,
        string databaseName,
        string collectionName)
    {
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<User>(collectionName);
    }

    public async Task<User> GetByIdAsync(string id)
    {
        var filter = Builders<User>.Filter.Eq("_id", ObjectId.Parse(id));
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task AddAsync(User entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(string id, User updatedUser)
    {
        var filter = Builders<User>.Filter.Eq("_id", ObjectId.Parse(id));
        await _collection.ReplaceOneAsync(filter, updatedUser);
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<User>.Filter.Eq("_id", ObjectId.Parse(id));
        await _collection.DeleteOneAsync(filter);
    }
}