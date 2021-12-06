using System.Collections.Generic;
using Domain.Repository;
using Domain.Services;
using Domain.ValueObjects;
using Infrastructure.Errors;
using Models = Domain.Models;

namespace Services
{
  public class Book : IBook
  {
    private readonly IBooks _books;

    private readonly ILog _logService;

    public Book(IBooks books, ILog logService)
    {
      _books = books;
      _logService = logService;
    }

    public Models.Book Add(Models.Book book)
    {
      var addedBook = _books.Add(book);

      _logService.Add(LogType.Create, "livro", 1);

      return addedBook;
    }

    public IResponse<Models.Book> Get(int id)
    {
      var response = new Response<Models.Book>();
      var book = _books.Get(id);

      if (book == null)
        response.Error = new ErrorObjectNotFound("Livro");

      else
        response.Result = book;

      return response;
    }

    public IEnumerable<Models.Book> GetAll() => _books.GetAll();

    public IResponse<Models.Book> Remove(int id)
    {
      var response = new Response<Models.Book>();
      var book = _books.Get(id);

      if (book == null)
        response.Error = new ErrorObjectNotFound("Livro");

      else
        _books.Remove(book);

      return response;
    }

    public IResponse<Models.Book> Update(Models.Book book)
    {
      var response = new Response<Models.Book>();
      var bookFound = _books.Get(book.Id);

      if (bookFound == null)
        response.Error = new ErrorObjectNotFound("Livro");

      else
      {
        bookFound.UpdateValues(book);
        _books.Update(book);
      }

      return response;
    }
  }
}