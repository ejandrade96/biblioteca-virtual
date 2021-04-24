using FluentAssertions;
using Xunit;

namespace Tests.Unit
{
  public class Password
  {
    [Fact]
    public void Deve_Gerar_Hash_De_Senha()
    {
      var hash = Infrastructure.Password.GenerateHash("123456");

      hash.Should().NotBe("123456");
      hash.Should().NotBeEmpty();
    }

    [Fact]
    public void Deve_Retornar_Verdadeiro_Quando_Passar_Um_Hash_Que_Corresponde_A_Uma_Senha()
    {
      var isValid = Infrastructure.Password.Validate("pMt6WXGnAFrN1o13CIDRGw==.Bc8/fYrDFfyw576GfZnlEgnYIqZfszuKEErs2agPgRA=", "123456");

      isValid.Should().BeTrue();
    }

    [Fact]
    public void Deve_Retornar_Falso_Quando_Passar_Um_Hash_Que_Nao_Corresponde_A_Uma_Senha()
    {
      var isValid = Infrastructure.Password.Validate("pMt6WXGnAFrN1o13CIDRGw==.Bc8/fYrDFfyw576GfZnlEgnYIqZfszuKEErs2agPgRA=", "123");

      isValid.Should().BeFalse();
    }
  }
}