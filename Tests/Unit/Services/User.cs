using Domain.ValueObjects;
using Xunit;
using Service = Services;
using Models = Domain.Models;
using Moq;
using Domain.Services;
using Domain.Repository;
using DTOs = Domain.DTOs;
using FluentAssertions;
using Infrastructure.Errors;

namespace Tests.Unit.Services
{
  public class User
  {
    private readonly IUser _service;

    private readonly Mock<IUsers> _users;

    private readonly Mock<IToken> _tokenService;

    public User()
    {
      _tokenService = new Mock<IToken>();
      _users = new Mock<IUsers>();
      _service = new Service.User(_users.Object, _tokenService.Object);
    }

    [Fact]
    public void Deve_Autenticar_Um_Usuario_Quando_Enviar_Dados_Certos()
    {
      var user = new Models.User
        (
          "Elton Andrade",
          "ejandrade",
          "elton@live.com",
          "pMt6WXGnAFrN1o13CIDRGw==.Bc8/fYrDFfyw576GfZnlEgnYIqZfszuKEErs2agPgRA=",
          AccessLevel.Administrator
        )
      { Id = 1 };

      var userData = new DTOs.User
      {
        Login = "ejandrade",
        Password = "123456"
      };

      _users.Setup(repository => repository.First(user => user.Login == userData.Login)).Returns(user);

      _tokenService.Setup(tokenService => tokenService.GenerateToken(user)).Returns("0ebf9yudsayugdyt784781y7842050720bd800e==.f9yuds781y7842050720bayu");

      var response = _service.Authenticate(userData);
      var userFound = response.Result;

      userFound.Id.Should().BeGreaterThan(0);
      userFound.Name.Should().Be("Elton Andrade");
      userFound.Login.Should().Be("ejandrade");
      userFound.Email.Should().Be("elton@live.com");
      userFound.Password.Should().BeNullOrWhiteSpace();
      userFound.AccessLevel.ToString().Should().Be("Administrator");
      userFound.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_Tentar_Autenticar_Um_Usuario_Inexistente()
    {
      var userData = new DTOs.User
      {
        Login = "ejandrade",
        Password = "123456"
      };

      var response = _service.Authenticate(userData);

      response.Error.Message.Should().Be("Login inválido(a)!");
      response.Error.StatusCode.Should().Be(400);
      response.Error.GetType().Should().Be(typeof(ErrorInvalidAttribute));
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_Tentar_Autenticar_Um_Usuario_Com_Senha_Invalida()
    {
      var user = new Models.User
        (
          "Elton Andrade",
          "ejandrade",
          "elton@live.com",
          "bHBlUVj1rFG48+Bd+4+yGA==.7KQGnYFMukLhkfSqhbfGhtqtqELUntz4AbGhrrqspLs=",
          AccessLevel.Administrator
        )
      { Id = 1 };

      var userData = new DTOs.User
      {
        Login = "ejandrade",
        Password = "123456"
      };

      _users.Setup(repository => repository.First(user => user.Login == userData.Login)).Returns(user);

      var response = _service.Authenticate(userData);

      response.Error.Message.Should().Be("Senha inválido(a)!");
      response.Error.StatusCode.Should().Be(400);
      response.Error.GetType().Should().Be(typeof(ErrorInvalidAttribute));
    }

    [Fact]  
    public void Deve_Retornar_Um_Usuario_Por_Login()
    {
      var user = new Models.User
        (
          "Elton Andrade",
          "ejandrade",
          "elton@live.com",
          "bHBlUVj1rFG48+Bd+4+yGA==.7KQGnYFMukLhkfSqhbfGhtqtqELUntz4AbGhrrqspLs=",
          AccessLevel.Administrator
        )
      { Id = 1 };

      _users.Setup(repository => repository.First(user => user.Login == "ejandrade")).Returns(user);

      var response = _service.GetByLogin("ejandrade");
      var userFound = response.Result;

      response.Error.Should().BeNull();
      userFound.Should().NotBeNull();
      userFound.Id.Should().NotBe(null);
      userFound.Id.Should().BeGreaterThan(0);
      userFound.Name.Should().NotBeNullOrWhiteSpace();
      userFound.Login.Should().NotBeNullOrWhiteSpace();
      userFound.Email.Should().NotBeNullOrWhiteSpace();
      userFound.AccessLevel.ToString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_Tentar_Buscar_Um_Usuario_Inexistente_Por_Login()
    {
      var response = _service.GetByLogin("xpto");

      response.Error.Message.Should().Be("Usuário não encontrado(a)!");
      response.Error.StatusCode.Should().Be(404);
      response.Error.GetType().Should().Be(typeof(ErrorObjectNotFound));
    }
  }
}