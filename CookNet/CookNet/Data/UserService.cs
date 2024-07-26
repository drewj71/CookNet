using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using CookNet.Data;
using Microsoft.EntityFrameworkCore.Internal;

public interface IUserService
{
    Task<ApplicationUser> GetUserAsync();
    Task<string> GetUsernameAsync();
    Task<ApplicationUser> GetUserByUsernameAsync(string username);
    Task<ApplicationUser> GetUserByIdAsync(string userId);
    Task<ApplicationUser> GetCurrentUserAsync(ClaimsPrincipal user);
}

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DbContextFactory _contextFactory;

    public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, DbContextFactory contextFactory)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _contextFactory = contextFactory;
    }

    public async Task<ApplicationUser> GetUserAsync()
    {
        return await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
    }

    public async Task<string> GetUsernameAsync()
    {
        var user = await GetUserAsync();
        return user != null ? await _userManager.GetUserNameAsync(user) : null;
    }

    public async Task<ApplicationUser> GetUserByUsernameAsync(string username)
    {
        return await _userManager.FindByNameAsync(username);
    }

    public async Task<ApplicationUser> GetUserByIdAsync(string userId)
    {
        using (var context = _contextFactory.CreateDbContext())
        {
            return await context.Users.FindAsync(userId);
        }
    }

    public async Task<ApplicationUser> GetCurrentUserAsync(ClaimsPrincipal user)
    {
        var userId = _userManager.GetUserId(user);
        return await GetUserByIdAsync(userId);
    }
}
