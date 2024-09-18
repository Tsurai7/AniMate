using Backend.Domain.Models;
using MongoDB.Driver;

namespace AccountService.Infrastructure.Repositories;

public class UserRepository
{
    private readonly IMongoCollection<User> _users;

    private const string ConnectionString =
        "mongodb+srv://animate-rw-user:Wf4sZc5GprjGp1yl@cluster0.kbrgw.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";

    public UserRepository()
    {
        var client = new MongoClient(ConnectionString);
        
        var database = client.GetDatabase("MyAppDb");
        
        _users = database.GetCollection<User>("Users");
    }
}