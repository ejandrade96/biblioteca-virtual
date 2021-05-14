using System.Collections.Generic;
using System.Linq;
using Domain.Services;

namespace webapp.ViewModels.Book
{
  public class IndexViewModel
  {
    private readonly IBook _service;

    public BookViewModel Book { get; set; }

    public List<BookViewModel> Books { get; set; }

    public IndexViewModel() { }

    public IndexViewModel(IBook service)
    { 
      _service = service;

      var book = new Domain.Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1) { Id = 1 };
      var book2 = new Domain.Models.Book("Clean Code 2", "Robert C. Martin", "39821389173", 478, 2) { Id = 2 };
      var books = new List<Domain.Models.Book> { book, book2 };

      Books = books.Select(book => new BookViewModel
      {
        Id = book.Id,
        Author = book.Author,
        Edition = book.Edition,
        ISBN = book.ISBN,
        Pages = book.Pages,
        Title = book.Title,
        Borrowed = true
      }).ToList();

      Books[1].Borrowed = false;
    }
  }
}