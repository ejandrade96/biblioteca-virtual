using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models = Domain.Models;

namespace Infrastructure.Mappings
{
  public class Log : IEntityTypeConfiguration<Models.Log>
  {
    public void Configure(EntityTypeBuilder<Models.Log> builder)
    {
      builder.ToTable("Logs");

      builder.HasKey(log => log.Id);
      builder.Property(log => log.LogDate).IsRequired();
      builder.Property(log => log.Description).IsRequired();
      builder.HasOne(log => log.User);
    }
  }
}