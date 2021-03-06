using System.Linq;
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

    public int Add(T entity) => _repository.Add(entity);

    public T Get(int id) => _repository.Get(id);

    public IQueryable<T> GetAll() => _repository.GetAll();

    public void Remove(T entity) => _repository.Remove(entity);

    public void Update(T entity) => _repository.Update(entity);
  }
}