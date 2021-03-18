using System.Collections.Generic;
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

    public T Add(T entity)
    {
      _dataset.Add(entity);
      _context.SaveChanges();
      return entity;
    }

    public T Get(int id) => _dataset.Find(id);

    public IEnumerable<T> GetAll() => _dataset;

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