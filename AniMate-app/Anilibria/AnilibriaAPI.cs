﻿using AniMate_app.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace AniMate_app.Anilibria
{
    public static class AnilibriaAPI
    {
        private const string BaseQueryAddress =
            "https://api.anilibria.tv/v3/";

        private static readonly HttpClient _httpClient = new();

        public static async Task<TitleRequestDto> GetTitle(string titleName)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync($"{BaseQueryAddress}title?code={titleName}");

            string jsonInfo = await response.Content.ReadAsStringAsync();

            TitleRequestDto title = JsonConvert.DeserializeObject<TitleRequestDto>(jsonInfo);

            return title;
        }
        
        public static async Task<ObservableCollection<string>> GetGenres()
        {
            using HttpResponseMessage response = await _httpClient.GetAsync($"{BaseQueryAddress}genres");

            string jsonInfo = await response.Content.ReadAsStringAsync();

            ObservableCollection<string> genres = JsonConvert.DeserializeObject<ObservableCollection<string>>(jsonInfo);

            return genres;
        }

        public static async Task<List<TitleRequestDto>> GetTilesByGenre(string genre)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync($"{BaseQueryAddress}title/search?genres={genre}");

            string jsonInfo = await response.Content.ReadAsStringAsync();

            List<TitleRequestDto> titles = JsonConvert.DeserializeObject<SearchDto>(jsonInfo).Titles;

            return titles;
        }

        public static async Task<EpisodeDto> GetEpisode(string titleName, int episodeOrdinal)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync($"{BaseQueryAddress}title?code={titleName}&filter=player.list[{episodeOrdinal}]");

            string jsonString = await response.Content.ReadAsStringAsync();

            JObject jsonInfo = JObject.Parse(jsonString);

            JToken episodeObject = jsonInfo["player"]["list"][episodeOrdinal];

            EpisodeDto episode = new()
            {
                Ordinal = (int)episodeObject["episode"],
                Uuid = (string)episodeObject["uuid"],
                Fhd = (string)episodeObject["hls"]["fhd"],
                Hd = (string)episodeObject["hls"]["hd"],
                Sd = (string)episodeObject["hls"]["sd"]
            };

            return episode;
        }

        public static async Task<List<TitleRequestDto>> Search(string titleName)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync($"{BaseQueryAddress}title/search?search={titleName}");

            string jsonInfo = await response.Content.ReadAsStringAsync();

            List<TitleRequestDto> titles = JsonConvert.DeserializeObject<SearchDto>(jsonInfo).Titles;

            return titles;
        }
    }
}