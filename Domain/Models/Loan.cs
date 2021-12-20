using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
  public class Loan : EntityBase
  {
    [Required]
    public Student Student { get; protected set; }
    
    [Required]
    public Book Book { get; protected set; }
    
    public DateTime LoanDate { get; protected set; }

    public DateTime ReturnDate { get; protected set; }
    
    protected Loan()
    {
    }

    public Loan(Student student, Book book)
    {
      Student = student;
      Book = book;
      LoanDate = DateTime.Now;
    }
  }
}