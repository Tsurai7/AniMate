using AniMate_backend.Models.TitleInfo;

namespace AniMate_backend.Models.Response
{
    public class TitleResponseDto
    {
        public long Id { get; set; }

        public string? Code { get; set; }

        public NamesDto Names { get; set; }

        public List<string> Genres { get; set; }

        public PlayerDto Player { get; set; }
    }
}
