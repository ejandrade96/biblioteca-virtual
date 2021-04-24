using Domain.Repository;
using Domain.Services;
using Infrastructure;
using Infrastructure.Errors;
using DTOs = Domain.DTOs;

namespace Services
{
  public class User : IUser
  {
    private readonly IToken _tokenService;

    private readonly IUsers _users;

    public User(IUsers users, IToken tokenService)
    {
      _tokenService = tokenService;
      _users = users;
    }

    public IResponse<DTOs.User> Authenticate(DTOs.User userData)
    {
      var response = new Response<DTOs.User>();

      var user = _users.First(x => x.Login == userData.Login);

      if (user == null)
      {
        response.Error = new ErrorInvalidAttribute("Login");
        return response;
      }

      bool isValidPassword = Password.Validate(user.Password, userData.Password);

      if (isValidPassword)
      {
        var token = _tokenService.GenerateToken(user);
        
        response.Result = new DTOs.User
        {
          Id = user.Id,
          Name = user.Name,
          Login = user.Login,
          Email = user.Email,
          Token = token,
          AccessLevel = user.AccessLevel.ToString()
        };
      }

      else
        response.Error = new ErrorInvalidAttribute("Senha");

      return response;
    }
  }
}