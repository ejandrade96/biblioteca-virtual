using System;
using System.Collections.Generic;
using Domain.Repository;
using Domain.Services;
using Domain.ValueObjects;
using Infrastructure.Errors;
using Infrastructure.Helpers;
using Models = Domain.Models;

namespace Services
{
  public class Book : ServiceBase<Models.Book>, IBook
  {
    private readonly IBooks _books;

    private readonly ILog _logService;

    public Book(IBooks books, ILog logService) : base(books)
    {
      _books = books;
      _logService = logService;
    }

    public Models.Book Add(Models.Book book)
    {
      var addedBook = _books.Add(book);

      _logService.Add(LogType.Create, "livro", addedBook.Id);

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

    public IEnumerable<Models.Book> GetAllWithLoans() => _books.GetAllWithLoans();

    public IEnumerable<Models.Book> GetAllWithLoansWithStudent() => _books.GetAllWithLoansWithStudent();

    public IEnumerable<IGroupingResponse<DateTime, Models.Book>> GetNumberBooksAddedInPeriod(int days)
    {
      var startDay = DateTime.Now.AddDays(-(days - 1)).StartOfDay();
      var endDay = DateTime.Now.EndOfDay();

      return base.GetNumberRecordsAddedInPeriod(x => x.CreatedAt >= startDay && x.CreatedAt <= endDay, x => x.CreatedAt.Date);
    }

    public IResponse<Models.Book> GetWithLoans(int id)
    {
      var response = new Response<Models.Book>();
      var book = _books.GetWithLoans(id);

      if (book == null)
        response.Error = new ErrorObjectNotFound("Livro");

      else
        response.Result = book;

      return response;
    }

    public IResponse<Models.Book> Remove(int id)
    {
      var response = new Response<Models.Book>();
      var book = _books.Get(id);

      if (book == null)
        response.Error = new ErrorObjectNotFound("Livro");

      else
      {
        _books.Remove(book);
        _logService.Add(LogType.Delete, "livro", id);
      }

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
        _books.Update(bookFound);
        _logService.Add(LogType.Update, "livro", book.Id);
      }

      return response;
    }
  }
}