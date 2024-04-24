using Newtonsoft.Json;

namespace AniMate_app.Services.AccountService.Dtos;

public record SignInResponse
{
    [JsonProperty("access_token")]
    public string? access_token { get; set; }
    
    [JsonProperty("email")]
    public string? email { get; set; }
}