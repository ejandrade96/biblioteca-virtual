namespace Domain.Models
{
  public class Contact
  {
    public string Email { get; protected set; }

    public string Telephone { get; protected set; }

    public string CellPhone { get; protected set; }

    public Contact(string email, string telephone)
    {
      Email = email;
      Telephone = telephone;
    }

    public void SetCellPhone(string cellPhone) => CellPhone = cellPhone;
  }
}