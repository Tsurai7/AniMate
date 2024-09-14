using System.Collections.Generic;
using System.Threading.Tasks;
using AniMate_app.DTOs.Anime;

namespace AniMate_app.Interfaces;

public interface IAnimeClient
{
    Task<TitleDto> GetTitleByCode(string code);
    Task<List<TitleDto>> GetTitlesByCode(List<string> codes, int skip = 0, int count = 6);
    Task<List<TitleDto>> GetTitlesByName(string name, int skip = 0, int count = 6);
    Task<List<TitleDto>> GetUpdates(int skip = 0, int count = 6);
    Task<List<string>> GetAllGenres();
    Task<List<TitleDto>> GetTitlesByGenre(string genre, int skip = 0, int count = 1);
}