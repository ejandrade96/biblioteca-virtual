using Domain.Errors;

namespace Infrastructure.Errors
{
  public class Error : IError
  {
    public string Message { get; set; }

    public int StatusCode { get; set; }

    public Error(string message)
    {
      Message = message;
      StatusCode = 400;
    }
  }
}