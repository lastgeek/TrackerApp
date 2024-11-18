using Microsoft.AspNetCore.Identity;
using TrackerApp.DataAccess;

namespace TrackerApp.Components.Account
{
    internal sealed class IdentityUserAccessor(UserManager<UserEntity> userManager, IdentityRedirectManager redirectManager)
    {
        public async Task<UserEntity> GetRequiredUserAsync(HttpContext context)
        {
            var user = await userManager.GetUserAsync(context.User);

            if (user is null)
            {
                redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
            }

            return user;
        }
    }
}
