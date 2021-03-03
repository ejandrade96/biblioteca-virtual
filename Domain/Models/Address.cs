using Domain.ValueObjects;

namespace Domain.Models
{
  public class Address
  {
    public string ZipCode { get; protected set; }

    public StreetType StreetType { get; protected set; }

    public string Street { get; protected set; }

    public int Number { get; protected set; }

    public string Complement { get; protected set; }

    public string District { get; protected set; }

    public string City { get; protected set; }

    public State State { get; protected set; }
    
    public Address(string zipCode, StreetType streetType, string street, int number, string complement, string district, string city, State state)
    {
      ZipCode = zipCode;
      StreetType = streetType;
      Street = street;
      Number = number;
      Complement = complement;
      District = district;
      City = city;
      State = state;
    }
  }
}