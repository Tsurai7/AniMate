namespace AniMate_app.Dto
{
    public record EpisodeDto
    {
        public int Ordinal { get; set; }
        public string Title { get; set; }
        public string Fhd { get; set; }
        public string Hd { get; set; }
        public string Sd { get; set; }
    }
}
