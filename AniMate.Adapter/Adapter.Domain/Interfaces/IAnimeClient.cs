using Adapter.Domain.Models.Responses;

namespace Adapter.Domain.Interfaces;

public interface IAnimeClient
{
    public Task<GetTitleResponse> GetTitleByCode(string code);
    public Task<GetTitlesResponse> GetTitlesByGenre(string genre);
    public Task<GetTitlesUpdatesResponse> GetUpdatedTitles();
    public Task<SearchTitlesResponse> SearchTitlesByName(string name);
}