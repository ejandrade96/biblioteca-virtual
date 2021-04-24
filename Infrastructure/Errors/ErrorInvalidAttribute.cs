using Domain.Errors;

namespace Infrastructure.Errors
{
  public class ErrorInvalidAttribute : IError
  {
    public string Message { get; set; }

    public int StatusCode { get; set; }
    
    public ErrorInvalidAttribute(string attribute)
    {
      Message = $"{attribute} inválido(a)!";
      StatusCode = 400;
    }
  }
}