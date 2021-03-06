using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models = Domain.Models;

namespace Infrastructure.Mappings
{
  public class Loan : IEntityTypeConfiguration<Models.Loan>
  {
    public void Configure(EntityTypeBuilder<Models.Loan> builder)
    {
      builder.ToTable("Loans");

      builder.HasKey(loan => loan.Id);
      builder.Property(loan => loan.LoanDate).IsRequired();
      builder.Property(loan => loan.ReturnDate).IsRequired();
      builder.HasOne(loan => loan.Student);
      builder.HasOne(loan => loan.Book);
    }
  }
}