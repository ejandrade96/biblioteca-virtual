using Domain.Models;

namespace Domain.Services
{
  public interface IToken
  {
    string GenerateToken(User user);
  }
}