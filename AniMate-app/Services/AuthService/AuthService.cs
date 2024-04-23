using System.Text;
using Newtonsoft.Json;

namespace AniMate_app.Services.AuthService;

public class AuthService
{
    private readonly HttpClient _httpClient;

    private const string _url = "http://10.0.2.2:5100";

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<string> GetStringFromApi()
    {
        string jsonInfo = "test";
        try
        {
            using HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/test");

            jsonInfo = await response.Content.ReadAsStringAsync();

        }
        catch (Exception e)
        {
            return e.Message;
        }
        
        return jsonInfo;
    }
    
    public async Task<string> SignIn(string email, string password)
    {
        string jsonInfo = "test";

        try
        {
            var authData = new
            {
                email,
                password
            };
            
            var jsonContent = JsonConvert.SerializeObject(authData);

            var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/signIn", requestContent);

            jsonInfo = await response.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return jsonInfo;
    }
}