using Microsoft.AspNetCore.Identity;

namespace Seedium.Repositories.Interface;

public interface IJwtTokenRepository
{
    string GenerateJwtToken(IdentityUser user, List<string> roles);
}
