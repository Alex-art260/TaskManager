using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using TaskManager.Interfaces;
using TaskManager.Models;
using Microsoft.AspNetCore.Mvc;
using TaskManager.ViewModel;

namespace TaskManager.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> GetCurrentUser()
        {
            if (_httpContextAccessor.HttpContext.User != null)
            {
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

                if (user != null)
                    return user;
                else
                    return null;
            }
            return null;
        }
    }
}
