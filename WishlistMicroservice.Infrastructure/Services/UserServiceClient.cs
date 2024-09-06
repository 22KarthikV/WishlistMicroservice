using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace WishlistMicroservice.Infrastructure.Services
{
    public interface IUserServiceClient
    {
        Task<LoginResponse> LoginAsync(string username, string password);
        Task<UserInfo> GetUserInfoAsync(string sessionId);
    }

    public class UserServiceClient : IUserServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly string _userServiceBaseUrl;

        public UserServiceClient(HttpClient httpClient, IMemoryCache cache, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _cache = cache;
            _userServiceBaseUrl = configuration["UserService:BaseUrl"];
        }

        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_userServiceBaseUrl}/api/user/login", new { username, password });

            if (!response.IsSuccessStatusCode)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

            // Cache the user info
            _cache.Set(loginResponse.SessionId, new UserInfo { UserId = loginResponse.UserId }, TimeSpan.FromMinutes(30));

            return loginResponse;
        }

        public async Task<UserInfo> GetUserInfoAsync(string sessionId)
        {
            if (_cache.TryGetValue(sessionId, out UserInfo cachedUserInfo))
            {
                return cachedUserInfo;
            }

            var response = await _httpClient.GetAsync($"{_userServiceBaseUrl}/api/user/info?sessionId={sessionId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new UnauthorizedAccessException("Invalid session");
            }

            var userInfo = await response.Content.ReadFromJsonAsync<UserInfo>();

            // Cache the user info
            _cache.Set(sessionId, userInfo, TimeSpan.FromMinutes(30));

            return userInfo;
        }
    }

    public class LoginResponse
    {
        public Guid UserId { get; set; }
        public string SessionId { get; set; }
        public string Token { get; set; }
    }

    public class UserInfo
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
    }
}