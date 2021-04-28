using System;
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
      builder.Property(student => student.Status).HasConversion(status => status.ToString(),
                                                                status => (Status)Enum.Parse(typeof(Status), status))
                                                 .HasColumnName("Status")
                                                 .IsRequired();

      builder.OwnsOne(student => student.Contact, contact => 
      {
        contact.Property(contact => contact.Email).HasColumnName("Email").IsRequired();
        contact.Property(contact => contact.Telephone).HasColumnName("Telephone").IsRequired();
        contact.Property(contact => contact.CellPhone).HasColumnName("CellPhone");
      });
      
      builder.OwnsOne(student => student.Address, address =>
      {
        address.Property(address => address.ZipCode).HasColumnName("ZipCode").IsRequired();
        address.Property(address => address.StreetType).HasConversion(streetType => streetType.Code, code => new StreetType(code)).HasColumnName("StreetType").IsRequired();
        address.Property(address => address.Street).HasColumnName("Street").IsRequired();
        address.Property(address => address.Number).HasColumnName("Number").IsRequired();
        address.Property(address => address.Complement).HasColumnName("Complement");
        address.Property(address => address.District).HasColumnName("District").IsRequired();
        address.Property(address => address.City).HasColumnName("City").IsRequired();
        address.Property(address => address.State).HasConversion(state => state.Acronym, acronym => new State(acronym)).HasColumnName("State").IsRequired();
      });
    }
  }
}