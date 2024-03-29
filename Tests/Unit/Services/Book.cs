using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Repository;
using Domain.Services;
using Domain.ValueObjects;
using FluentAssertions;
using Infrastructure.Errors;
using Infrastructure.Helpers;
using Moq;
using Xunit;
using Models = Domain.Models;
using Service = Services;

namespace Tests.Unit.Services
{
  public class Book
  {
    private readonly IBook _service;

    private readonly Mock<ILog> _logService;

    private readonly Mock<IBooks> _books;

    public Book()
    {
      _books = new Mock<IBooks>();
      _logService = new Mock<ILog>();
      _service = new Service.Book(_books.Object, _logService.Object);
    }

    [Fact]
    public void Deve_Cadastrar_Um_Livro_Quando_Enviar_Dados_Certos()
    {
      var bookToAdd = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);

      _books.Setup(repository => repository.Add(It.IsAny<Models.Book>()))
        .Returns(new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1) { Id = 1 });

      var book = _service.Add(bookToAdd);

      book.Id.Should().BeGreaterThan(0);
    }

    [Fact]
    public void Deve_Listar_Todos_Livros()
    {
      var book = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1) { Id = 1 };
      book.SetImage("~/image.jpg");
      var books = new List<Models.Book> { book, book };

      _books.Setup(repository => repository.GetAll()).Returns(books.AsQueryable());

      var booksFound = _service.GetAll();

      booksFound.Should().HaveCount(2);
      booksFound.ToList().ForEach(book =>
      {
        book.Id.Should().NotBe(null);
        book.Id.Should().BeGreaterThan(0);
        book.Title.Should().NotBeNullOrWhiteSpace();
        book.Author.Should().NotBeNullOrWhiteSpace();
        book.ISBN.Should().NotBeNullOrWhiteSpace();
        book.Pages.Should().NotBe(null);
        book.Pages.Should().BeGreaterThan(0);
        book.Edition.Should().NotBe(null);
        book.Edition.Should().BeGreaterThan(0);
        book.Image.Should().NotBeNullOrWhiteSpace();
        book.CreatedAt.Should().NotBe(DateTime.MinValue);
      });
    }

    [Fact]
    public void Deve_Atualizar_Um_Livro_Quando_Enviar_Dados_Certos()
    {
      var book = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);

      _books.Setup(repository => repository.Get(It.IsAny<int>())).Returns(book);

      var response = _service.Update(book);

      response.Error.Should().BeNull();
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_Tentar_Atualizar_Um_Livro_Inexistente()
    {
      var book = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);

      var response = _service.Update(book);

      response.Error.Message.Should().Be("Livro não encontrado(a)!");
      response.Error.StatusCode.Should().Be(404);
      response.Error.GetType().Should().Be(typeof(ErrorObjectNotFound));
    }

    [Fact]
    public void Deve_Deletar_Um_Livro()
    {
      var book = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);

      _books.Setup(repository => repository.Get(It.IsAny<int>())).Returns(book);

      var response = _service.Remove(1);

      response.Error.Should().BeNull();
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_Tentar_Deletar_Um_Livro_Inexistente()
    {
      var response = _service.Remove(1);

      response.Error.Message.Should().Be("Livro não encontrado(a)!");
      response.Error.StatusCode.Should().Be(404);
      response.Error.GetType().Should().Be(typeof(ErrorObjectNotFound));
    }

    [Fact]
    public void Deve_Retornar_Um_Livro_Por_Id()
    {
      var book = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1) { Id = 1 };
      book.SetImage("~/image.jpg");

      _books.Setup(repository => repository.Get(It.IsAny<int>())).Returns(book);

      var response = _service.Get(1);
      var bookFound = response.Result;

      bookFound.Id.Should().NotBe(null);
      bookFound.Id.Should().BeGreaterThan(0);
      bookFound.Title.Should().NotBeNullOrWhiteSpace();
      bookFound.Author.Should().NotBeNullOrWhiteSpace();
      bookFound.ISBN.Should().NotBeNullOrWhiteSpace();
      bookFound.Pages.Should().NotBe(null);
      bookFound.Pages.Should().BeGreaterThan(0);
      bookFound.Edition.Should().NotBe(null);
      bookFound.Edition.Should().BeGreaterThan(0);
      bookFound.Image.Should().NotBeNullOrWhiteSpace();
      book.CreatedAt.Should().NotBe(DateTime.MinValue);
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_Tentar_Buscar_Um_Livro_Inexistente_Por_Id()
    {
      var response = _service.Get(1);

      response.Error.Message.Should().Be("Livro não encontrado(a)!");
      response.Error.StatusCode.Should().Be(404);
      response.Error.GetType().Should().Be(typeof(ErrorObjectNotFound));
    }

    [Fact]
    public void Deve_Retornar_Um_Livro_Com_Emprestimos_Por_Id()
    {
      var book = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1) { Id = 1 };

      var contact = new Models.Contact("joao.villar@live.com", "1154218547");
      contact.SetCellPhone("11996582134");
      var streetType = StreetType.StreetTypes.First(x => x.Code == "R");
      var state = State.States.First(x => x.Acronym == "SP");
      var address = new Models.Address("09421700", streetType, "dos Vianas", 412, "Centro", "São Bernardo do Campo", state);
      address.SetComplement("AP Torre 1");
      var student = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address) { Id = 1 };

      var loan = new Models.Loan(student, book) { Id = 1 };

      book.SetLoan(loan);
      book.SetImage("~/image.jpg");

      _books.Setup(repository => repository.GetWithLoans(It.IsAny<int>())).Returns(book);

      var response = _service.GetWithLoans(1);
      var bookFound = response.Result;

      bookFound.Id.Should().NotBe(null);
      bookFound.Id.Should().BeGreaterThan(0);
      bookFound.Title.Should().NotBeNullOrWhiteSpace();
      bookFound.Author.Should().NotBeNullOrWhiteSpace();
      bookFound.ISBN.Should().NotBeNullOrWhiteSpace();
      bookFound.Pages.Should().NotBe(null);
      bookFound.Pages.Should().BeGreaterThan(0);
      bookFound.Edition.Should().NotBe(null);
      bookFound.Edition.Should().BeGreaterThan(0);
      bookFound.Image.Should().NotBeNullOrWhiteSpace();
      book.CreatedAt.Should().NotBe(DateTime.MinValue);
      bookFound.Loans.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_Tentar_Buscar_Um_Livro_Com_Emprestimos_Inexistente_Por_Id()
    {
      var response = _service.GetWithLoans(1);

      response.Error.Message.Should().Be("Livro não encontrado(a)!");
      response.Error.StatusCode.Should().Be(404);
      response.Error.GetType().Should().Be(typeof(ErrorObjectNotFound));
    }

    [Fact]
    public void Deve_Retornar_Um_Agrupamento_Da_Quantidade_De_Livros_Adicionados_Em_Um_Determinado_Periodo_De_Dias()
    {
      var numberOfDays = 5;
      var startDay = DateTime.Now.AddDays(-(numberOfDays - 1)).StartOfDay();
      var endDay = DateTime.Now.EndOfDay();

      var book = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      var firstDay = DateTime.Now.AddDays(-(numberOfDays - 1)).StartOfDay();
      book.SetCreatedAt(firstDay);

      var book2 = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      var secondDay = DateTime.Now.AddDays(-(numberOfDays - 2));
      book2.SetCreatedAt(secondDay);
      var book3 = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      book3.SetCreatedAt(secondDay.EndOfDay());

      var book4 = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      var lastDay = DateTime.Now.EndOfDay();
      book4.SetCreatedAt(lastDay);

      var books = new List<Models.Book> { book, book2, book3, book4 };

      _books.Setup(repository => repository.FindAll(x => x.CreatedAt >= startDay && x.CreatedAt <= endDay)).Returns(books.AsQueryable());

      var groupingNewBooks = _service.GetNumberBooksAddedInPeriod(numberOfDays);

      groupingNewBooks.Should().HaveCount(3);
      groupingNewBooks.ElementAt(0).Key.Should().Be(firstDay.Date);
      groupingNewBooks.ElementAt(0).Elements.Count().Should().Be(1);
      groupingNewBooks.ElementAt(1).Key.Should().Be(secondDay.Date);
      groupingNewBooks.ElementAt(1).Elements.Count().Should().Be(2);
      groupingNewBooks.ElementAt(2).Key.Should().Be(lastDay.Date);
      groupingNewBooks.ElementAt(2).Elements.Count().Should().Be(1);
    }

    [Fact]
    public void Deve_Retornar_Um_Agrupamento_Da_Quantidade_De_Livros_Adicionados_Em_Um_Determinado_Periodo_De_Meses()
    {
      var numberOfMonths = 12;
      var firstDayOfFirstMonth = DateTime.Now.AddMonths(-11).FirstDayOfMonth();
      var daySecondMonth = firstDayOfFirstMonth.AddMonths(1);
      var dayThirdMonth = daySecondMonth.AddMonths(1);
      var dayFourthMonth = dayThirdMonth.AddMonths(1);
      var dayFifthMonth = dayFourthMonth.AddMonths(1);
      var daySixthMonth = dayFifthMonth.AddMonths(1);
      var daySeventhMonth = daySixthMonth.AddMonths(1);
      var dayEighthMonth = daySeventhMonth.AddMonths(1);
      var dayNinthMonth = dayEighthMonth.AddMonths(1);
      var dayTenthMonth = dayNinthMonth.AddMonths(1);
      var dayEleventhMonth = dayTenthMonth.AddMonths(1);
      var dayTwelfthMonth = dayEleventhMonth.AddMonths(1);

      var book = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      book.SetCreatedAt(firstDayOfFirstMonth);
      var book2 = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      book2.SetCreatedAt(firstDayOfFirstMonth);
      var book3 = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      book3.SetCreatedAt(daySecondMonth);
      var book4 = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      book4.SetCreatedAt(dayThirdMonth);
      var book5 = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      book5.SetCreatedAt(dayFourthMonth);
      var book6 = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      book6.SetCreatedAt(dayFourthMonth);
      var book7 = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      book7.SetCreatedAt(dayFourthMonth);
      var book8 = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      book8.SetCreatedAt(dayFifthMonth);
      var book9 = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      book9.SetCreatedAt(daySixthMonth);
      var book10 = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      book10.SetCreatedAt(daySeventhMonth);
      var book11 = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      book11.SetCreatedAt(dayEighthMonth);
      var book12 = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      book12.SetCreatedAt(dayNinthMonth);
      var book13 = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      book13.SetCreatedAt(dayTenthMonth);
      var book14 = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      book14.SetCreatedAt(dayEleventhMonth);
      var book15 = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      book15.SetCreatedAt(dayEleventhMonth);
      var book16 = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      book16.SetCreatedAt(dayTwelfthMonth);

      var books = new List<Models.Book>
      {
        book, book2, book3, book4, book5, book6, book7, book8, book9, book10, book11, book12, book13, book14, book15, book16
      };

      _books.Setup(repository => repository.FindAll(x => x.CreatedAt >= firstDayOfFirstMonth && x.CreatedAt <= DateTime.Now)).Returns(books.AsQueryable());

      var groupingNewBooks = _service.GetNumberBooksAddedInPeriodOfMonths(numberOfMonths);

      groupingNewBooks.Should().HaveCount(12);
      groupingNewBooks.ElementAt(0).Key.KeyOne.Should().Be(firstDayOfFirstMonth.Month);
      groupingNewBooks.ElementAt(0).Key.KeyTwo.Should().Be(firstDayOfFirstMonth.Year);
      groupingNewBooks.ElementAt(0).Elements.Count().Should().Be(2);
      groupingNewBooks.ElementAt(1).Key.KeyOne.Should().Be(daySecondMonth.Month);
      groupingNewBooks.ElementAt(1).Key.KeyTwo.Should().Be(daySecondMonth.Year);
      groupingNewBooks.ElementAt(1).Elements.Count().Should().Be(1);
      groupingNewBooks.ElementAt(2).Key.KeyOne.Should().Be(dayThirdMonth.Month);
      groupingNewBooks.ElementAt(2).Key.KeyTwo.Should().Be(dayThirdMonth.Year);
      groupingNewBooks.ElementAt(2).Elements.Count().Should().Be(1);
      groupingNewBooks.ElementAt(3).Key.KeyOne.Should().Be(dayFourthMonth.Month);
      groupingNewBooks.ElementAt(3).Key.KeyTwo.Should().Be(dayFourthMonth.Year);
      groupingNewBooks.ElementAt(3).Elements.Count().Should().Be(3);
      groupingNewBooks.ElementAt(4).Key.KeyOne.Should().Be(dayFifthMonth.Month);
      groupingNewBooks.ElementAt(4).Key.KeyTwo.Should().Be(dayFifthMonth.Year);
      groupingNewBooks.ElementAt(4).Elements.Count().Should().Be(1);
      groupingNewBooks.ElementAt(5).Key.KeyOne.Should().Be(daySixthMonth.Month);
      groupingNewBooks.ElementAt(5).Key.KeyTwo.Should().Be(daySixthMonth.Year);
      groupingNewBooks.ElementAt(5).Elements.Count().Should().Be(1);
      groupingNewBooks.ElementAt(6).Key.KeyOne.Should().Be(daySeventhMonth.Month);
      groupingNewBooks.ElementAt(6).Key.KeyTwo.Should().Be(daySeventhMonth.Year);
      groupingNewBooks.ElementAt(6).Elements.Count().Should().Be(1);
      groupingNewBooks.ElementAt(7).Key.KeyOne.Should().Be(dayEighthMonth.Month);
      groupingNewBooks.ElementAt(7).Key.KeyTwo.Should().Be(dayEighthMonth.Year);
      groupingNewBooks.ElementAt(7).Elements.Count().Should().Be(1);
      groupingNewBooks.ElementAt(8).Key.KeyOne.Should().Be(dayNinthMonth.Month);
      groupingNewBooks.ElementAt(8).Key.KeyTwo.Should().Be(dayNinthMonth.Year);
      groupingNewBooks.ElementAt(8).Elements.Count().Should().Be(1);
      groupingNewBooks.ElementAt(9).Key.KeyOne.Should().Be(dayTenthMonth.Month);
      groupingNewBooks.ElementAt(9).Key.KeyTwo.Should().Be(dayTenthMonth.Year);
      groupingNewBooks.ElementAt(9).Elements.Count().Should().Be(1);
      groupingNewBooks.ElementAt(10).Key.KeyOne.Should().Be(dayEleventhMonth.Month);
      groupingNewBooks.ElementAt(10).Key.KeyTwo.Should().Be(dayEleventhMonth.Year);
      groupingNewBooks.ElementAt(10).Elements.Count().Should().Be(2);
      groupingNewBooks.ElementAt(11).Key.KeyOne.Should().Be(dayTwelfthMonth.Month);
      groupingNewBooks.ElementAt(11).Key.KeyTwo.Should().Be(dayTwelfthMonth.Year);
      groupingNewBooks.ElementAt(11).Elements.Count().Should().Be(1);
    }
  }
}