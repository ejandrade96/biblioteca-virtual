namespace Domain.Models
{
  public class Contact : EntityBase
  {
    public string Email { get; set; }

    public string Telephone { get; set; }

    public string CellPhone { get; set; }

    public Contact(string email, string telephone, string cellPhone)
    {
      Email = email;
      Telephone = telephone;
      CellPhone = cellPhone;
    }
  }
}