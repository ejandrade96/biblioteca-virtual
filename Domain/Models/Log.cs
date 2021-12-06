using System;

namespace Domain.Models
{
  public class Log : EntityBase
  {
    public DateTime LogDate { get; protected set; }

    public string Description { get; protected set; }

    public virtual User User { get; protected set; }

    public Log(User user, DateTime logDate, string description)
    {
        User = user;
        LogDate = logDate;
        Description = description;
    }

    protected Log()
    {
    }
  }
}