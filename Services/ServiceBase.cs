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

    /// <summary>
    /// Returns all records that match the predicate, grouping them by groupByCond
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="groupByCond"></param>
    protected IEnumerable<IGroupingResponse<DateTime, T>> GetNumberRecordsAddedInPeriod(Expression<Func<T, bool>> predicate, Func<T, DateTime> groupByCond)
         => _repository.FindAll(predicate)
                       .ToList()
                       .GroupBy(groupByCond)
                       .Select(x => new GroupingResponse<DateTime, T>
                       {
                         Key = x.Key,
                         Elements = x.ToList()
                       })
                       .OrderBy(x => x.Key);

    /// <summary>
    /// Returns all records that match the predicate, grouping them by groupDoubleByCond
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="groupDoubleByCond"></param>
    protected IEnumerable<IGroupingResponse<IGroupingResponseKey<int, int>, T>> GetNumberRecordsAddedInPeriodOfMonths(Expression<Func<T, bool>> predicate, Func<T, dynamic> groupDoubleByCond)
         => _repository.FindAll(predicate)
                       .ToList()
                       .GroupBy(groupDoubleByCond)
                       .Select(x => new GroupingResponse<IGroupingResponseKey<int, int>, T>
                       {
                         Key = new GroupingResponseKey<int, int> { KeyOne = x.Key.Month, KeyTwo = x.Key.Year },
                         Elements = x.ToList()
                       })
                       .OrderBy(x => x.Key.KeyTwo)
                       .ThenBy(x => x.Key.KeyOne);
  }
}