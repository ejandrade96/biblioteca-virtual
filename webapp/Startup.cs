using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using webapp.Extensions;

namespace webapp
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton<IConfiguration>(Configuration);
      services.AddControllersWithViews();
      services.AddServices();
      services.AddRepositories();
      services.AddDbContext<BibliotecaVirtualContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BibliotecaVirtualContext")));
      services.AddAutoMapper(typeof(Mappings.Student));

      var key = Encoding.ASCII.GetBytes(Configuration["JwtConfiguration:SecretKey"]);
      services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddCookie(config =>
         {
           config.Cookie.Name = "access_token";
         })
      .AddJwtBearer(x =>
      {
        x.Events = new JwtBearerEvents
        {
          OnMessageReceived = context =>
          {
            var value = "";
            context.Request.Cookies.TryGetValue("access_token", out value);
            context.Token = value;
            return Task.CompletedTask;
          }
        };
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = true,
          ValidIssuer = Configuration["JwtConfiguration:Issuer"],
          ValidateAudience = true,
          ValidAudience = Configuration["JwtConfiguration:Audience"]
        };
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseStatusCodePages(context =>
        {
          var request = context.HttpContext.Request;
          var response = context.HttpContext.Response;

          if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
          // you may also check requests path to do this only for specific methods       
          // && request.Path.Value.StartsWith("/specificPath")
          {
            response.Redirect("/Account/Login");
          }

          return Task.CompletedTask;
        });

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      if (env.IsDevelopment())
      {
        app.UseCors(x => x
                  .AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader());
      }
      else
      {
        app.UseCors(options => options.WithOrigins("http://52.91.231.38/")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
      }

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
