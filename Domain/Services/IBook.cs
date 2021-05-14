using System.Collections.Generic;
using Domain.Models;

namespace Domain.Services
{
  public interface IBook
  {
    Book Add(Book book);

    IResponse<Book> Get(int id);

    IEnumerable<Book> GetAll();

    IResponse<Book> Remove(int id);

    IResponse<Book> Update(Book book);
  }
}