using BoardGameBrowserAPI.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace BoardGameBrowserAPI.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(APIUserDTO user);
    }
}
