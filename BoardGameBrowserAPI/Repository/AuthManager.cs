using AutoMapper;
using BoardGameBrowserAPI.Contracts;
using BoardGameBrowserAPI.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace BoardGameBrowserAPI.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthManager(IMapper mapper, UserManager<IdentityUser> userManager)
        {
            this._mapper = mapper;
            this._userManager = userManager;
        }
        public async Task<IEnumerable<IdentityError>> Register(APIUserDTO userDTO)
        {
            var user = _mapper.Map<IdentityUser>(userDTO);
            
            var result = await _userManager.CreateAsync(user, userDTO.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Administrator");
            }

            return result.Errors;
        }
    }
}
