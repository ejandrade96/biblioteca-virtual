using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models = Domain.Models;

namespace Infrastructure.Mappings
{
  public class Book : IEntityTypeConfiguration<Models.Book>
  {
    public void Configure(EntityTypeBuilder<Models.Book> builder)
    {
      builder.ToTable("Books");

      builder.HasKey(book => book.Id);
      builder.Property(book => book.Title).IsRequired();
      builder.Property(book => book.Author).IsRequired();
      builder.Property(book => book.ISBN).IsRequired();
      builder.Property(book => book.Pages).IsRequired();
      builder.Property(book => book.Edition).IsRequired();
      builder.Property(book => book.Image);
    }
  }
}