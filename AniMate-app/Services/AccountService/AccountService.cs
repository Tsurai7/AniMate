using System.Text;
using AniMate_app.Services.AccountService.Dtos;
using Newtonsoft.Json;

namespace AniMate_app.Services.AccountService;

public class AccountService
{
    private readonly HttpClient _httpClient;

    private const string _url = "http://10.0.2.2:5100";

    public AccountService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<ProfileDto> GetProfileInfo(string token)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        
        using HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/profile");

        string jsonInfo = await response.Content.ReadAsStringAsync();
        
        ProfileDto profileDto = JsonConvert.DeserializeObject<ProfileDto>(jsonInfo);
        
        return profileDto;
    }
    
    public async Task<AuthResponse> SignIn(string email, string password)
    {
        var authData = new
        {
            email,
            password
        };
            
        string jsonContent = JsonConvert.SerializeObject(authData);

        var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        
        using HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/signIn", requestContent);

        string jsonInfo = await response.Content.ReadAsStringAsync();
        
        AuthResponse res = JsonConvert.DeserializeObject<AuthResponse>(jsonInfo);
        
        return res;
    }

    public async Task<AuthResponse> SignUp(string email, string password, string username)
    {
        var authData = new
        {
            username,
            email,
            password,
        };
            
        string jsonContent = JsonConvert.SerializeObject(authData);

        var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        using HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/signUp", requestContent);

        string jsonInfo = await response.Content.ReadAsStringAsync();
        
        AuthResponse res = JsonConvert.DeserializeObject<AuthResponse>(jsonInfo);
        
        return res;
    }
}