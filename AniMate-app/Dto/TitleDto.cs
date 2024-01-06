namespace AniMate_app.Dto
{
    public record TitleDto
    {
        public string RuName { get; set; }
        public List<string> Genres { get; set; }
        public string Player { get; set; }
        public List<EpisodeDto> Episodes { get; set;}
    }
}
