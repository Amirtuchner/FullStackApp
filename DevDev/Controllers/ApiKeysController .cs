using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using DevDev.Services;
using DevDev.Models;

namespace DevDev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiKeysController : ControllerBase
    {
        private readonly IApiKeyService _apiKeyService;

        private readonly string _jwtSecret = "YourSecretKeyForJWToiasfhoushufgiah4762627r4hqR";

        public ApiKeysController(IApiKeyService apiKeyService)
        {
            _apiKeyService = apiKeyService;
        }

        [HttpPost]
        public IActionResult CreateApiKey([FromBody] ApiKeyRequest request)
        {
            var apiKey = _apiKeyService.CreateApiKey(request.UserId, request.Permissions);
            return Ok(new { apiKey });
        }


        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateRequest request)
        {
            var userId = _apiKeyService.AuthenticateApiKey(request.ApiKey);
            if (userId == null) return Unauthorized();

            var token = GenerateJwtToken(userId);
            return Ok(new { token });
        }


        [HttpDelete("{apiKey}")]
        public IActionResult RevokeApiKey(string apiKey)
        {
            _apiKeyService.RevokeApiKey(apiKey);
            return NoContent();
        }


        [HttpGet]
        public IActionResult GetUserApiKeys([FromQuery] string userId)
        {
            var apiKeys = _apiKeyService.GetUserApiKeys(userId);
            return Ok(apiKeys);
        }


        private string GenerateJwtToken(string userId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: new[] { new Claim("userId", userId) },
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
