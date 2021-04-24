using Domain.ValueObjects;

namespace Domain.Models
{
  public class User : EntityBase
  {
    public string Name { get; protected set; }

    public string Login { get; protected set; }

    public string Email { get; protected set; }

    public string Password { get; protected set; }

    public AccessLevel AccessLevel { get; protected set; }

    protected User()
    {
    }

    public User(string name, string login, string email, string password, AccessLevel accessLevel)
    {
      Name = name;
      Login = login;
      Email = email;
      Password = password;
      AccessLevel = accessLevel;
    }
  }
}