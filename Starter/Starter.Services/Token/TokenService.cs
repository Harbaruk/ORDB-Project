using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Starter.Common.DomainTaskStatus;
using Starter.DAL.Entities;
using Starter.DAL.Infrastructure;
using Starter.Services.Crypto;
using Starter.Services.Token.Models;

namespace Starter.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICryptoContext _cryptoContext;
        private readonly DomainTaskStatus _taskStatus;
        private readonly IOptions<JwtOptions> _options;

        public TokenService(IUnitOfWork unitOfWork, ICryptoContext cryptoContext, DomainTaskStatus taskStatus, IOptions<JwtOptions> options)
        {
            _unitOfWork = unitOfWork;
            _cryptoContext = cryptoContext;
            _taskStatus = taskStatus;
            _options = options;
        }

        public TokenModel GetRefreshToken(RefreshTokenModel token)
        {
            var user = _unitOfWork.Repository<UserEntity>().Include(x => x.Tokens).FirstOrDefault(x => x.Tokens.Any(y => y.Token == token.RefreshToken));

            if (user == null)
            {
                _taskStatus.AddError("refresh_token", "Invalid refresh token");
                return null;
            }
            var refreshToken = _unitOfWork.Repository<RefreshTokenEntity>().Set.FirstOrDefault(x => x.Token == token.RefreshToken);
            _unitOfWork.Repository<RefreshTokenEntity>().Delete(refreshToken);

            return BuildToken(user);
        }

        public TokenModel GetToken(LoginCredentials loginCredentials)
        {
            var user = _unitOfWork.Repository<UserEntity>().Include(x => x.Tokens).FirstOrDefault(x => x.Email == loginCredentials.Email);

            if (user == null)
            {
                _taskStatus.AddError("credentials", "Invalid credentials");
                return null;
            }

            if (_cryptoContext.ArePasswordsEqual(loginCredentials.Password, user.Password, user.Salt))
            {
                return BuildToken(user);
            }

            _taskStatus.AddError("password", "Invalid password");
            return null;
        }

        public TokenModel GetTokenForConfirmedUser(Guid guid)
        {
            var user = _unitOfWork.Repository<UserEntity>().Include(x => x.Tokens).FirstOrDefault(x => x.Id == guid);

            if (user == null)
            {
                _taskStatus.AddUnkeyedError("Invalid user");
                return null;
            }
            return BuildToken(user);
        }

        private TokenModel BuildToken(UserEntity user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]{
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Firstname + " " + user.Lastname)
                };

            // TODO Change expiration date
            var token = new JwtSecurityToken(
              issuer: _options.Value.ValidIssuer,
              audience: _options.Value.ValidAudience,
              claims: claims,
              expires: DateTime.Now.AddMinutes(_options.Value.LifetimeInMinutes),
              signingCredentials: creds);

            var refreshToken = Guid.NewGuid().ToString();

            user.Tokens.Add(new RefreshTokenEntity { Token = refreshToken });
            _unitOfWork.SaveChanges();

            return new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = DateTimeOffset.Now.AddMinutes(30),
                IssueAt = DateTimeOffset.Now,
                RefreshToken = refreshToken,
                Id = user.Id
            };
        }
    }
}