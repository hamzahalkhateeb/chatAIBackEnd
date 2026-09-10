using Microsoft.AspNetCore.Mvc;
using backEnd.Modules;
using backEnd.Modules.Authentication;
using backEnd.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using backEnd.Modules.Utils;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Identity;
using Superpower.Model;
using backEnd.Data;

namespace backEnd.Modules.Authentication
{

    
    
    public class AuthService
    {

        //dependancy injection
        private readonly IConfiguration _config;
        private readonly AppDbContext _db;

        public AuthService(IConfiguration config, AppDbContext db)
        {
            _config = config;
            _db = db;
        }

        
        public async Task<string> GenerateRefreshToken(Guid Id)
        {
            Console.WriteLine($"AuthService.GenerateRefreshToken: generating a refresh token for user with Id: {Id}");

            var rawToken = HelperMethods.GenerateSecureRandomString();
            Console.WriteLine($"AuthService.GenerateRefreshToken: generated a raw token: {rawToken}");

            var tokenHash = HelperMethods.HashToken(rawToken);

            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = Id,
                TokenHash = tokenHash,
                CreatedAt = DateTimeOffset.UtcNow,
                ExpiresAt = DateTimeOffset.UtcNow.AddDays(7),
                RevokedAt = null,
                ReplacedByTokenId = null,
                
            };

             Console.WriteLine($"AuthService.GenerateRefreshToken: final refresh token {refreshToken}");

             _db.RefreshTokens.Add(refreshToken);
             await _db.SaveChangesAsync();

             return rawToken;

        }

    }
}