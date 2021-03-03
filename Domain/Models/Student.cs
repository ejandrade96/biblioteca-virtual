namespace Domain.Models
{
  public class Student : EntityBase
  {
    public string Name { get; protected set; }
    
    public string Login { get; protected set; }
    
    public int Record { get; protected set; }
    
    public Contact Contact { get; protected set; }

    public Address Address { get; protected set; }
    
    public Student(string name, string login, int record, Contact contact, Address address)
    {
      Name = name;
      Login = login;
      Record = record;
      Contact = contact;
      Address = address;
    }
  }
}