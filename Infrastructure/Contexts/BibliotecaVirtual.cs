using System;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Contexts
{
  public class BibliotecaVirtualContext : DbContext
  {
    public BibliotecaVirtualContext(DbContextOptions<BibliotecaVirtualContext> options) : base(options)
    {
    }

    public BibliotecaVirtualContext()
    {
    }

    public DbSet<Student> Students { get; set; }

    public DbSet<Book> Books { get; set; }

    public DbSet<Loan> Loans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                                          .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                          .AddJsonFile("appsettings.json")
                                          .Build();

        optionsBuilder.UseSqlServer(configuration.GetConnectionString("BibliotecaVirtualContext"));
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.EnableSensitiveDataLogging();
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<Student>(new Mappings.Student().Configure);
      modelBuilder.Entity<Book>(new Mappings.Book().Configure);
      modelBuilder.Entity<Loan>(new Mappings.Loan().Configure);
    }
  }
}