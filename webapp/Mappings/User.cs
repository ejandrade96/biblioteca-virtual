using AutoMapper;
using webapp.ViewModels.Account;
using DTOs = Domain.DTOs;

namespace webapp.Mappings
{
  public class User : Profile
  {
    public User()
    {
      CreateMap<LoginViewModel, DTOs.User>().ReverseMap();
    }
  }
}