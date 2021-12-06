using System.Linq;
using System.Security.Claims;
using Domain.Services;
using Microsoft.AspNetCore.Http;

namespace Services
{
  public class ApplicationUser : IApplicationUser
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApplicationUser(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public string GetCurrentUserLogin() => ((ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity).Claims.First(x => x.Type == "user_login").Value;
  }
}