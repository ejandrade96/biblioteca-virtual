using AutoMapper;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapp.ViewModels.Account;
using DTOs = Domain.DTOs;
using webapp.Helpers;

namespace webapp.Controllers
{
  [AllowAnonymous]
  public class AccountController : ControllerBase
  {
    private readonly IUser _userService;

    private readonly IMapper _mapper;

    public AccountController(IUser userService, IMapper mapper)
    {
      _userService = userService;
      _mapper = mapper;
    }

    public IActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel loginViewModel)
    {
      var userData = _mapper.Map<DTOs.User>(loginViewModel);

      var response = _userService.Authenticate(userData);

      if (response.HasError())
      {
        TempData["Error"] = response.Error.Message;
        return View();
      }

      var user = response.Result;
      LoggedInUser = user.ToLoggedInUserViewModel();
      AccessToken = user.Token;

      return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
      LoggedInUser = null;
      AccessToken = null;
      return RedirectToAction("Login");
    }
  }
}