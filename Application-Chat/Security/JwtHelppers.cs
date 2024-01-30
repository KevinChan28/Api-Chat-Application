using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application_Chat.Security
{
	public static class JwtHelppers
	{

		public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, Guid Id)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim("Id",userAccounts.Id.ToString()),
				new Claim(ClaimTypes.Name, userAccounts.UserName),
				new Claim(ClaimTypes.Email, userAccounts.Email),
				new Claim(ClaimTypes.Expiration, DateTime.Now.AddDays(1).ToString("G")),
				new Claim(ClaimTypes.Role, userAccounts.Rol)
			};

			return claims;
		}

		public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out Guid Id)
		{
			Id = Guid.NewGuid();

			return GetClaims(userAccounts, Id);
		}

		public static string GenerateToken(UserTokens modeL, JwtSettings jwtSettings)
		{
			try
			{
				UserTokens userToken = new UserTokens();

				if (userToken == null)
				{
					throw new ArgumentNullException(nameof(userToken));
				}
				//Obtain key secret
				var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);

				Guid Id;
				//Expire
				DateTime expireTime = DateTime.Now.AddDays(1);

				//Validty
				userToken.Validty = expireTime.TimeOfDay;

				//GENERATE OUR JWT

				var jwToken = new JwtSecurityToken(
					issuer: jwtSettings.ValidIssuer,
					audience: jwtSettings.ValidAudience,
					claims: GetClaims(modeL, out Id),
					notBefore: new DateTimeOffset(DateTime.Now).DateTime,
					expires: new DateTimeOffset(expireTime).DateTime,
					signingCredentials: new SigningCredentials
					(
						new SymmetricSecurityKey(key),
						SecurityAlgorithms.HmacSha256
					)
					);
				userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwToken);

				return userToken.Token;
			}
			catch (Exception ex)
			{

				throw new Exception("Error generating thw JWT", ex);
			}
		}
	}
}
