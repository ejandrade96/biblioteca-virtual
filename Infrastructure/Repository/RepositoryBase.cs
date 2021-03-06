using System.Linq;
using Domain.Models;
using Domain.Repository;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
  public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
  {
    private readonly BibliotecaVirtualContext _context;

    protected DbSet<T> _dataset;

    public RepositoryBase(BibliotecaVirtualContext context)
    {
      _context = context;
      _dataset = _context.Set<T>();
    }

    public int Add(T entity)
    {
      _dataset.Add(entity);
      _context.SaveChanges();

      return entity.Id;
    }

    public T Get(int id) => _dataset.Find(id);

    public IQueryable<T> GetAll() => _dataset;

    public void Remove(T entity)
    {
      _dataset.Remove(entity);
      _context.SaveChanges();
    }

    public void Update(T entity)
    {
      _dataset.Update(entity);
      _context.SaveChanges();
    }
  }
}