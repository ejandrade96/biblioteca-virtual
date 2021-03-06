using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models = Domain.Models;

namespace Infrastructure.Mappings
{
  public class Student : IEntityTypeConfiguration<Models.Student>
  {
    public void Configure(EntityTypeBuilder<Models.Student> builder)
    {
      builder.ToTable("Students");

      builder.HasKey(student => student.Id);
      builder.Property(student => student.Name).IsRequired();
      builder.Property(student => student.Login).IsRequired();
      builder.Property(student => student.Record).IsRequired();

      builder.OwnsOne(student => student.Contact, contact => 
      {
        contact.Property(contact => contact.Email).IsRequired();
        contact.Property(contact => contact.Telephone).IsRequired();
        contact.Property(contact => contact.CellPhone);
      });
      
      builder.OwnsOne(student => student.Address, address =>
      {
        address.Property(address => address.ZipCode).IsRequired();
        address.Property(address => address.StreetType).IsRequired().HasConversion(streetType => streetType.Code, code => new StreetType(code)).HasColumnName("StreetType");
        address.Property(address => address.Street).IsRequired();
        address.Property(address => address.Number).IsRequired();
        address.Property(address => address.Complement);
        address.Property(address => address.District).IsRequired();
        address.Property(address => address.City).IsRequired();
        address.Property(address => address.State).IsRequired().HasConversion(state => state.Acronym, acronym => new State(acronym)).HasColumnName("State");
      });
    }
  }
}