using Domain.Services;
using Services;
using Microsoft.Extensions.DependencyInjection;

namespace webapp.Extensions
{
  public static class Services
  {
    public static void AddServices(this IServiceCollection services)
    {
      services.AddTransient<IStudent, Student>();
    }
  }
}