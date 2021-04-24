using System;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models = Domain.Models;

namespace Infrastructure.Mappings
{
  public class User : IEntityTypeConfiguration<Models.User>
  {
    public void Configure(EntityTypeBuilder<Models.User> builder)
    {
      builder.ToTable("Users");

      builder.HasKey(user => user.Id);
      builder.Property(user => user.Name).IsRequired();
      builder.Property(user => user.Login).IsRequired();
      builder.Property(user => user.Email).IsRequired();
      builder.Property(user => user.Password).IsRequired();
      builder.Property(user => user.AccessLevel).HasConversion(accessLevel => accessLevel.ToString(),
                                                               accessLevel => (AccessLevel)Enum.Parse(typeof(AccessLevel), accessLevel))
                                                .HasColumnName("AccessLevel")
                                                .IsRequired();
    }
  }
}