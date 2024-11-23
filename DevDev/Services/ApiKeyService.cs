using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Concurrent;


namespace DevDev.Services
{
    public interface IApiKeyService 
    {
        string CreateApiKey(string userId, string[] permissions);
        string AuthenticateApiKey(string apiKey);
        void RevokeApiKey(string apiKey);
        IEnumerable<ApiKey> GetUserApiKeys(string userId);
    }


    public class ApiKeyService : IApiKeyService
    {

        private readonly ConcurrentDictionary<string, ApiKey> _apiKeys = new();

        public string CreateApiKey(string userId, string[] permissions)
        {
            var apiKey = Guid.NewGuid().ToString();
            _apiKeys[apiKey] = new ApiKey
            {
                Key = apiKey,
                UserId = userId,
                Permissions = permissions,
                CreatedAt = DateTime.UtcNow,
                LastUsedAt = null,
                IsActive = true
            };
            return apiKey;
        }


        public string AuthenticateApiKey(string apiKey)
        {
            if (_apiKeys.TryGetValue(apiKey, out var key) && key.IsActive)
            {
                key.LastUsedAt = DateTime.UtcNow;
                return key.UserId;
            }
            return null;
        }

        public void RevokeApiKey(string apiKey)
        {
            if (_apiKeys.ContainsKey(apiKey))
            {
                _apiKeys[apiKey].IsActive = false;
            }
        }

        public IEnumerable<ApiKey> GetUserApiKeys(string userId)
        {
            return _apiKeys.Values.Where(k => k.UserId == userId);
        }
    }


    public class ApiKey
    {
        public string Key { get; set; }
        public string UserId { get; set; }
        public string[] Permissions { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUsedAt { get; set; }
        public bool IsActive { get; set; }
    }



}
