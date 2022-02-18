using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Models;
using Domain.Repository;
using Domain.Services;

namespace Services
{
  public abstract class ServiceBase<T> : IServiceBase<T> where T : EntityBase
  {
    protected IRepositoryBase<T> _repository;

    public ServiceBase(IRepositoryBase<T> repository)
    {
      _repository = repository;
    }

    public IEnumerable<IGroupingResponse<DateTime, T>> GetNumberRecordsAddedInPeriod(Expression<Func<T, bool>> predicate, Func<T, DateTime> groupByCond)
         => _repository.FindAll(predicate)
                       .ToList()
                       .GroupBy(groupByCond)
                       .Select(x => new GroupingResponse<DateTime, T>
                       {
                         Key = x.Key,
                         Elements = x.ToList()
                       })
                       .OrderBy(x => x.Key);
  }
}