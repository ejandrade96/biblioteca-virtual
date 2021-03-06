using Domain.Repository;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace webapp.Extensions
{
  public static class Repository
  {
    public static void AddRepositories(this IServiceCollection services)
    {
      services.AddTransient<IStudents, Students>();
    }
  }
}