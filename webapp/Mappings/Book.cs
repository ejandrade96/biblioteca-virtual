using AutoMapper;

namespace webapp.Mappings
{
  public class Book : Profile
  {
    public Book()
    {
      CreateMap<Domain.Models.Book, ViewModels.Book.BookViewModel>().ReverseMap();
    }
  }
}