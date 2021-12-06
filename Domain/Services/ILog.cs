using Domain.ValueObjects;

namespace Domain.Services
{
  public interface ILog
  {
    void Add(LogType logType, string entity, int entityId);
  }
}