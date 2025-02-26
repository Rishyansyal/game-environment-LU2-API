using System.Security.Claims;


/// <summary>
/// Based on the example code provided by Microsoft
/// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-context?view=aspnetcore-9.0&preserve-view=true
/// </summary>
public class AspNetIdentityAuthenticationService : IAuthenticationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AspNetIdentityAuthenticationService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc />
    public string? GetCurrentAuthenticatedUserId()
    {
        // Returns the aspnet_User.Id of the authenticated user
        return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
