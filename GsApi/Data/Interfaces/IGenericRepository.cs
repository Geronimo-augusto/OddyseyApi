using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace OddyseyApi.Data.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    Task SaveChangesAsync();
}
