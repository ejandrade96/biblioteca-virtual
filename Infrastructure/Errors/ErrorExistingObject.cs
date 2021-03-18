using Domain.Errors;

namespace Infrastructure.Errors
{
  public class ErrorExistingObject : IError
  {
    public string Message { get; set; }

    public int StatusCode { get; set; }

    public ErrorExistingObject(string entity, string attribute)
    {
      Message = $"{entity} já cadastrado(a) com este {attribute}!";
      StatusCode = 400;
    }
  }
}