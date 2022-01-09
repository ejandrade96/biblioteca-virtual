using System.Collections.Generic;
using System.Linq;
using Domain.Services;

namespace webapp.ViewModels.Book
{
  public class IndexViewModel
  {
    public BookViewModel Book { get; set; }

    public List<BookViewModel> Books { get; set; }

    public IndexViewModel() { }

    public IndexViewModel(IBook service)
    {
      Books = service.GetAllWithLoans().Select(book => new BookViewModel
      {
        Id = book.Id,
        Title = book.Title,
        Author = book.Author,
        ISBN = book.ISBN,
        Pages = book.Pages,
        Edition = book.Edition,
        Borrowed = book.IsBorrowed()
      }).ToList();
    }
  }
}