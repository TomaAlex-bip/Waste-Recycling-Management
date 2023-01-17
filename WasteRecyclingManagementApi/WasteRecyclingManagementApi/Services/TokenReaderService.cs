using System.IdentityModel.Tokens.Jwt;
using WasteRecyclingManagementApi.Core.Services;

namespace WasteRecyclingManagementApi.Services
{
    public class TokenReaderService : ITokenReaderService
    {
        public int GetUserId(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token.Replace("Bearer ", ""));

            int userId = -1;
            var subId = securityToken.Claims.FirstOrDefault(c => c.Type.Equals("sub"))?.Value;
            if (subId != null)
                userId = int.Parse(subId);

            return userId;
        }
    }
}
