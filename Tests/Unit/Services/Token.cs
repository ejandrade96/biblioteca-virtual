using Domain.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using Service = Services;
using Models = Domain.Models;
using Domain.ValueObjects;
using FluentAssertions;

namespace Tests.Unit.Services
{
  public class Token
  {
    private readonly IToken _service;

    private readonly Mock<IConfiguration> _config;

    private readonly Mock<IConfigurationSection> _configSection;

    public Token()
    {
      _config = new Mock<IConfiguration>();
      _configSection = new Mock<IConfigurationSection>();
      _service = new Service.Token(_config.Object);
    }

    [Fact]
    public void Deve_Ser_Possivel_Gerar_Um_Token_Com_Base_Em_Um_Usuario()
    {
      var user = new Models.User("Elton", "ejandrade", "elton@live.com", "123456", AccessLevel.Administrator);

      // _configSection.Setup(configSection => configSection.Value).Returns("0ebf94e00dca47ee82a8050720bd800e");
      // _config.Setup(x => x.GetSection("MySection:Value")).Returns(configSection.Object);
      _config.Setup(config => config["JwtConfiguration:SecretKey"]).Returns("0ebf9yudsayugdyt784781y7842050720bd800e");

      var token = _service.GenerateToken(user);

      token.Should().NotBeNullOrWhiteSpace();
    }
  }
}