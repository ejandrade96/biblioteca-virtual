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

    public Address(string zipCode, StreetType streetType, string street, int number, string district, string city, State state)
    {
      ZipCode = zipCode;
      StreetType = streetType;
      Street = street;
      Number = number;
      District = district;
      City = city;
      State = state;
    }

    public void SetComplement(string complement) => Complement = complement;

    public void SetStreetType(StreetType streetType) => StreetType = streetType;

    public void SetState(State state) => State = state;

    public void UpdateValues(Address address)
    {
      ZipCode = address.ZipCode;
      StreetType = address.StreetType;
      Street = address.Street;
      Number = address.Number;
      Complement = address.Complement;
      District = address.District;
      City = address.City;
      State = address.State;
    }
  }
}