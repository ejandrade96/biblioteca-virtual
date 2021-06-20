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
    }

    public void UpdateValues(Book book)
    {
      Title = book.Title;
      Author = book.Author;
      ISBN = book.ISBN;
      Pages = book.Pages;
      Edition = book.Edition;
    }

    public void SetImage(string image) => Image = image;
  }
}