using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AniMate_app.DTOs.Account;
using AniMate_app.DTOs.Auth;
using Newtonsoft.Json;

namespace AniMate_app.Services;

public class AccountService
{
    private readonly HttpClient _httpClient;

    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public AccountService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ProfileDto> GetProfileInfo(string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"account/profile");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);
        var jsonInfo = await response.Content.ReadAsStringAsync();
        var profileDto = JsonConvert.DeserializeObject<ProfileDto>(jsonInfo);
        
        return profileDto;
    }

    public async Task<bool> AddTitleToLiked(string token, string titleCode)
    {
        var request = new HttpRequestMessage(HttpMethod.Patch, $"addTitleToLiked");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var requestContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");
        request.Content = requestContent;
        requestContent.Headers.Add("titleCode", titleCode);

        var response = await _httpClient.SendAsync(request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RemoveTitleFromLiked(string token, string titleCode)
    {
        var request = new HttpRequestMessage(HttpMethod.Patch, $"removeTitleFromLiked");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var requestContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");
        request.Content = requestContent;
        
        requestContent.Headers.Add("titleCode", titleCode);

        var response = await _httpClient.SendAsync(request);
        
        return response.IsSuccessStatusCode;
    }

    public async Task<AuthResponse> SignIn(string email, string password)
    {
        var authData = new
        {
            email,
            password
        };

        var jsonContent = JsonConvert.SerializeObject(authData);
        var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"auth/signIn", requestContent);
        
        var jsonInfo = await response.Content.ReadAsStringAsync();
        
        return JsonConvert.DeserializeObject<AuthResponse>(jsonInfo);
    }

    public async Task<bool> AddTitleToHistory(string token, string titleCode)
    {
        var request = new HttpRequestMessage(HttpMethod.Patch, $"addTitleToHistory");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var requestContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");
        request.Content = requestContent;
        
        requestContent.Headers.Add("titleCode", titleCode);

        var response = await _httpClient.SendAsync(request);
        
        return response.IsSuccessStatusCode;
    }

    public async Task<AuthResponse> SignUp(string email, string password, string username)
    {
        var authData = new
        {
            username,
            email,
            password,
        };

        var jsonContent = JsonConvert.SerializeObject(authData);
        
        var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"auth/signUp", requestContent);
        
        var jsonInfo = await response.Content.ReadAsStringAsync();
        
        return JsonConvert.DeserializeObject<AuthResponse>(jsonInfo);
    }
}
