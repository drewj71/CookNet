using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using CookNet.Data;

public interface IUserService
{
    Task<ApplicationUser> GetUserAsync();
    Task<string> GetUsernameAsync();
    Task<ApplicationUser> GetUserByUsernameAsync(string username);
}

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
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
}
