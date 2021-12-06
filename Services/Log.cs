using System;
using Domain.Repository;
using Domain.Services;
using Domain.ValueObjects;
using Models = Domain.Models;

namespace Services
{
  public class Log : ILog
  {
    private readonly ILogs _logs;

    private readonly IUser _userService;

    private readonly IApplicationUser _applicationUser;

    public Log(ILogs logs, IUser userService, IApplicationUser applicationUser)
    {
      _logs = logs;
      _userService = userService;
      _applicationUser = applicationUser;
    }

    public void Add(LogType logType, string entity, int entityId)
    {
      var userLogin = _applicationUser.GetCurrentUserLogin();

      var response = _userService.GetByLogin(userLogin);
      var user = response.Result;

      var description = GetDescriptionByLogType(logType, entity, entityId);

      var log = new Models.Log(user, DateTime.Now, description);

      _ = _logs.Add(log);
    }

    private string GetDescriptionByLogType(LogType logType, string entity, int entityId)
    {
      switch (logType)
      {
        case LogType.Create:
          return $"Adicionou {entity} de id {entityId}";

        case LogType.Update:
          return $"Atualizou {entity} de id {entityId}";

        case LogType.Delete:
          return $"Removeu {entity} de id {entityId}";

        default:
          return "";
      }
    }
  }
}