using AniMate_backend.Models;

namespace AniMate_backend.Interfaces
{
    public interface IAnilibriaService
    {
        Task<Title> GetTitleByCode(string code);

        Task<List<Title>> GetAllTitlesByName(string name);

        //Task<List<Title>> GetTitlesByGenre(string genre);

        Task<List<string>> GetAllGenres();
    }
}
