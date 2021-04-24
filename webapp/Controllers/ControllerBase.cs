using Microsoft.AspNetCore.Mvc;
using webapp.ViewModels.Shared;
using Nancy.Json;
using Microsoft.AspNetCore.Mvc.Filters;

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