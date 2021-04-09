using AutoMapper;

namespace webapp.Mappings
{
  public class Student : Profile
  {
    public Student()
    {
      CreateMap<Domain.Models.Student, ViewModels.Student.StudentViewModel>()
      .ForPath(dest => dest.Contact.Email, opt => opt.MapFrom(src => src.Contact.Email))
      .ForPath(dest => dest.Contact.CellPhone, opt => opt.MapFrom(src => src.Contact.CellPhone))
      .ForPath(dest => dest.Contact.Telephone, opt => opt.MapFrom(src => src.Contact.Telephone))
      .ForPath(dest => dest.Address.ZipCode, opt => opt.MapFrom(src => src.Address.ZipCode))
      .ForPath(dest => dest.Address.StreetType, opt => opt.MapFrom(src => src.Address.StreetType.Code))
      .ForPath(dest => dest.Address.Street, opt => opt.MapFrom(src => src.Address.Street))
      .ForPath(dest => dest.Address.Number, opt => opt.MapFrom(src => src.Address.Number))
      .ForPath(dest => dest.Address.Complement, opt => opt.MapFrom(src => src.Address.Complement))
      .ForPath(dest => dest.Address.District, opt => opt.MapFrom(src => src.Address.District))
      .ForPath(dest => dest.Address.City, opt => opt.MapFrom(src => src.Address.City))
      .ForPath(dest => dest.Address.State, opt => opt.MapFrom(src => src.Address.State.Acronym))
      .ReverseMap();
    }
  }
}