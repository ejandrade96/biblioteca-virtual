using System;
using System.Collections.Generic;
using Domain.Models;

namespace Domain.Services
{
  public interface IBook : IServiceBase<Book>
  {
    Book Add(Book book);

    IResponse<Book> Get(int id);

    IEnumerable<Book> GetAll();

    IResponse<Book> Remove(int id);

    IResponse<Book> Update(Book book);

    IEnumerable<Book> GetAllWithLoans();

    IEnumerable<Book> GetAllWithLoansWithStudent();

    IResponse<Book> GetWithLoans(int id);

    IEnumerable<IGroupingResponse<DateTime, Book>> GetNumberBooksAddedInPeriod(int days);

    IEnumerable<IGroupingResponse<IGroupingResponseKey<int, int>, Book>> GetNumberBooksAddedInPeriodOfMonths(int months);
  }
}