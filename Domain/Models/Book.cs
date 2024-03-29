using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Models
{
  public class Book : EntityBase
  {
    public string Title { get; protected set; }

    public string Author { get; protected set; }

    public string ISBN { get; protected set; }

    public int Pages { get; protected set; }

    public int Edition { get; protected set; }

    public string Image { get; protected set; }

    public DateTime CreatedAt { get; set; }

    public List<Loan> Loans { get; protected set; } = new List<Loan>();

    protected Book()
    {
    }

    public Book(string title, string author, string isbn, int pages, int edition)
    {
      Title = title;
      Author = author;
      ISBN = isbn;
      Pages = pages;
      Edition = edition;
      CreatedAt = DateTime.Now;
    }

    public void UpdateValues(Book book)
    {
      Title = book.Title;
      Author = book.Author;
      ISBN = book.ISBN;
      Pages = book.Pages;
      Edition = book.Edition;
      Image = book.Image;
    }

    public void SetImage(string image) => Image = image;

    public void SetLoan(Loan loan) => Loans.Add(loan);

    public bool IsBorrowed()
    {
      var lastLoan = Loans.OrderByDescending(x => x.LoanDate).FirstOrDefault();

      if (lastLoan == null)
        return false;

      return lastLoan.ReturnDate.Equals(DateTime.MinValue) || lastLoan.ReturnDate == null;
    }

    public string GetLoanStudentLogin() => Loans.OrderByDescending(x => x.LoanDate).First().Student.Login;

    public int GetCurrentLoanId() => Loans.OrderByDescending(x => x.LoanDate).First().Id;

    public void SetCreatedAt(DateTime date) => CreatedAt = date;
  }
}