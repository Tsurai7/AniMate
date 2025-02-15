﻿using System.Text.Json.Serialization;

namespace AniMate_app.DTOs.Anime; 

public class EpisodeDto
{
    [JsonPropertyName("episode")]
    public double Ordinal { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("hls")]
    public HlsDto HlsUrls { get; init; }
}

