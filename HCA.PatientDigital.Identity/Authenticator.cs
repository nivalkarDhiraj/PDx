using HCA.PlatformDigital.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HCA.PatientDigital.Identity
{
    public class Authenticator : IAuthenticator
    {
        private readonly IConfiguration _config;
        private readonly string _encryptionKey;
        private readonly string _validIssuer;
        private readonly string _key;
        private readonly double _tokenExpireInMinute;
        private readonly string _email;
        private readonly string _accessKey;
        public Authenticator(IConfiguration configuration)
        {
            // to read configuration
            _config = configuration;
            _encryptionKey = _config["Encryption:Key"];
            _validIssuer = _config["IdentityConfiguration:Issuer"];
            _key = _config["IdentityConfiguration:Key"];            
            _tokenExpireInMinute = string.IsNullOrEmpty(_config["IdentityConfiguration:Value"]) ? 60 : Convert.ToDouble(_config["IdentityConfiguration:Value"]);
            _email = _config["IdentityConfiguration:email"];
            _accessKey = _config["IdentityConfiguration:accesskey"];

        }
        // Authenticate user credential with actual source of identity.
        public User Authenticate(User userDetails)
        {
            User user = null;

            //TODO: Demo purpose have used hardcoded credential, this needs to be validated with actual IDP.
            if (userDetails.EmailAddress == _email
                && Encryption.DecryptString(userDetails.Password, _encryptionKey) == Encryption.DecryptString(_accessKey, _encryptionKey))
            {
                // fill user details from identity source to create claim.
                user = new User { Id = 1, FirstName = "HCA", LastName = "Demo", EmailAddress = _email };
            }
            return user;
        }
        // Generate JWT token
        public string CreateToken(string emailAddress, string password)
        {

            //TODO: need to change this to RSA algo to make it more secure
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, emailAddress),
                    new Claim(JwtRegisteredClaimNames.Email, emailAddress),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            var token = new JwtSecurityToken(_validIssuer,
               _validIssuer,
                claims,
                expires: DateTime.Now.AddMinutes(_tokenExpireInMinute),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
