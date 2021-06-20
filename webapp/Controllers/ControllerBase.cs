using Microsoft.AspNetCore.Mvc;
using webapp.ViewModels.Shared;
using Nancy.Json;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace webapp.Controllers
{
  public abstract class ControllerBase : Controller
  {
    public LoggedInUserViewModel _LoggedInUser { get; set; }

    public LoggedInUserViewModel LoggedInUser
    {
      get
      {
        if (_LoggedInUser != null)
          return _LoggedInUser;

        if (string.IsNullOrEmpty(Request.Cookies["LoggedInUser"]))
          return null;

        return new JavaScriptSerializer().Deserialize<LoggedInUserViewModel>(Request.Cookies["LoggedInUser"]);
      }

      set
      {
        if (value == null)
          Response.Cookies.Delete("LoggedInUser");

        else
          Response.Cookies.Append("LoggedInUser", new JavaScriptSerializer().Serialize(value)); _LoggedInUser = value;
      }
    }

    public string _AccessToken { get; set; }

    public string AccessToken
    {
      get
      {
        if (_AccessToken != null)
          return _AccessToken;

        if (string.IsNullOrEmpty(Request.Cookies["access_token"]))
          return null;

        return new JavaScriptSerializer().Deserialize<string>(Request.Cookies["access_token"]);
      }

      set
      {
        if (value == null)
          Response.Cookies.Delete("access_token");

        else
          Response.Cookies.Append("access_token", value, new CookieOptions { HttpOnly = true, /*Secure = true*/ }); _AccessToken = value;
      }
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
      base.OnActionExecuting(context);
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
      TempData["LoggedInUser"] = new JavaScriptSerializer().Serialize(LoggedInUser);

      base.OnActionExecuted(context);
    }
  }
}