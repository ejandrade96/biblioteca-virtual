using Domain.Errors;

namespace Infrastructure.Errors
{
  public class ErrorObjectNotFound : IError
  {
    public string Message { get; set; }

    public int StatusCode { get; set; }

    public ErrorObjectNotFound(string entity)
    {
      Message = $"{entity} não encontrado(a)!";
      StatusCode = 404;
    }
  }
}