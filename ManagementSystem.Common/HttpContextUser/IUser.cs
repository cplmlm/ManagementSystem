using System.Collections.Generic;
using System.Security.Claims;
using ManagementSystem.Model;

namespace ManagementSystem.Common.HttpContextUser
{
    public interface IUser
    {
        string Name { get; }
        long Id { get; }
        long OrganizationId { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
        List<string> GetClaimValueByType(string ClaimType);

        string GetToken();
        List<string> GetUserInfoFromToken(string ClaimType);

        MessageModel<string> MessageModel { get; set; }
    }
}