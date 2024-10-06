using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities;

namespace Talabat.APIS.G02.Extensions {
    public static class UserManagerExtensions {

        public static async Task<AppUser> FindCurrentUserAddress(this UserManager<AppUser> userManager, ClaimsPrincipal User) {

            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await  userManager.Users.Include(x => x.Address).FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }
    }
}
