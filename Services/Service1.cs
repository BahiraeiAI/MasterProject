using System;
namespace MasterProject.Services
{
    public class Service1
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Service1(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AppUser> GetLoggedInUserAsync()
        {
            string? userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User); // Get the logged-in user's ID
            return await _userManager.FindByIdAsync(userId); // Retrieve the user from the database
        }
    }

}

