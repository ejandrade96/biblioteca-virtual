namespace Domain.DTOs
{
  public struct User
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Login { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string Token { get; set; }

    public string AccessLevel { get; set; }
  }
}