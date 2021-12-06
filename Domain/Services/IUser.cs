namespace Domain.Services
{
  public interface IUser
  {
    IResponse<DTOs.User> Authenticate(DTOs.User userData);
  
    IResponse<Models.User> GetByLogin(string login);
  }
}