using Adapter.Domain.Models.Responses;

namespace Adapter.Domain.Interfaces;

public interface IAnimeClient
{
    public Task<Title> GetTitleByCode(string code);
    public Task<List<Title>> GetTitlesByGenre(string genre, int skip = 0, int count = 1);
    public Task<List<Title>> GetUpdatedTitles(int skip = 0, int count = 6);
    public Task<List<Title>> SearchTitlesByName(string name, int skip = 0, int count = 6);
}