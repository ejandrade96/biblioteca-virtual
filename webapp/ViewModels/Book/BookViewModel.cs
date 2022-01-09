using Microsoft.AspNetCore.Http;

namespace webapp.ViewModels.Book
{
  public class BookViewModel : BookViewModelBase
  {
    public bool Borrowed { get; set; }
    
    public IFormFile Image { get; set; }

    public string ImagePath { get; set; }
  }
}