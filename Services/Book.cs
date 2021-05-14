using System.Collections.Generic;
using Domain.Repository;
using Domain.Services;
using Infrastructure.Errors;
using Models = Domain.Models;

namespace Services
{
  public class Book : IBook
  {
    private readonly IBooks _books;

    public Book(IBooks books)
    {
      _books = books;
    }

    public Models.Book Add(Models.Book book) => _books.Add(book);

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