using Backend.Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Backend.Infrastructure.Repositories;

public class AccountRepository : IMongoRepository<Account>
{
    private readonly IMongoCollection<Account> _collection;
    public AccountRepository(
        IMongoClient client,
        string databaseName,
        string collectionName)
    {
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<Account>(collectionName);
    }

    public async Task<Account> GetByIdAsync(string id)
    {
        var filter = Builders<Account>.Filter.Eq("_id", ObjectId.Parse(id));
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }
    
    public async Task<Account?> GetByEmailAsync(string email)
    {
        var filter = Builders<Account>.Filter.Eq(account => account.Email, email);
        return await _collection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task AddAsync(Account entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task UpdateOneAsync(Account updatedAccount)
    {
        var filter = Builders<Account>.Filter.Eq(account => account.Email, updatedAccount.Email);
        await _collection.ReplaceOneAsync(filter, updatedAccount);
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<Account>.Filter.Eq("_id", ObjectId.Parse(id));
        await _collection.DeleteOneAsync(filter);
    }
}