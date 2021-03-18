namespace Domain.Errors
{
  public interface IError
  {
    string Message { get; set; }

    int StatusCode { get; set; }
  }
}