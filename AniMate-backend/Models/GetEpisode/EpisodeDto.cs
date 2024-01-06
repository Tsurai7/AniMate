namespace AniMate_backend.Models.GetEpisode
{
    public record EpisodeDto
    {
        public int Ordinal { get; set; }
        public string? Name { get; set; }
        public string? Uuid { get; set; }

        public string? Fhd { get; set; }
        public string? Hd { get; set; }
        public string? Sd { get; set; }
    }
}
