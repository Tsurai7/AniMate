using System.Net.Http.Headers;
using System.Text;
using AniMate_app.DTOs.Account;
using AniMate_app.DTOs.Auth;
using Newtonsoft.Json;

namespace AniMate_app.Services
{
    public class AccountService
    {
        private readonly HttpClient _httpClient;
        private const string _url = "http://10.0.2.2:10100";

        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProfileDto> GetProfileInfo(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_url}/account/profile");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using HttpResponseMessage response = await _httpClient.SendAsync(request);
            string jsonInfo = await response.Content.ReadAsStringAsync();
            ProfileDto profileDto = JsonConvert.DeserializeObject<ProfileDto>(jsonInfo);
            return profileDto;
        }

        public async Task<bool> AddTitleToLiked(string token, string titleCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, $"{_url}/addTitleToLiked");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var requestContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            request.Content = requestContent;
            requestContent.Headers.Add("titleCode", titleCode);

            using HttpResponseMessage response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveTitleFromLiked(string token, string titleCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, $"{_url}/removeTitleFromLiked");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var requestContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            request.Content = requestContent;
            requestContent.Headers.Add("titleCode", titleCode);

            using HttpResponseMessage response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
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

            using HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/auth/signIn", requestContent);
            string jsonInfo = await response.Content.ReadAsStringAsync();
            AuthResponse res = JsonConvert.DeserializeObject<AuthResponse>(jsonInfo);
            return res;
        }

        public async Task<bool> AddTitleToHistory(string token, string titleCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, $"{_url}/addTitleToHistory");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var requestContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            request.Content = requestContent;
            requestContent.Headers.Add("titleCode", titleCode);

            using HttpResponseMessage response = await _httpClient.SendAsync(request);
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

            string jsonContent = JsonConvert.SerializeObject(authData);
            var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/auth/signUp", requestContent);
            string jsonInfo = await response.Content.ReadAsStringAsync();
            AuthResponse res = JsonConvert.DeserializeObject<AuthResponse>(jsonInfo);
            return res;
        }
    }
}
