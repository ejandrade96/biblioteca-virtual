namespace Domain.Models
{
  public class Contact
  {
    public string Email { get; protected set; }

    public string Telephone { get; protected set; }

    public string CellPhone { get; protected set; }

    public Contact(string email, string telephone, string cellPhone)
    {
      Email = email;
      Telephone = telephone;
      CellPhone = cellPhone;
    }
  }
}