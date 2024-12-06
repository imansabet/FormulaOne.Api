using FormulaOne.DataService.Data;
using FormulaOne.DataService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FormulaOne.DataService.Repositories;

public class GenericRepository<T>(ILogger logger, AppDbContext context) : IGenericRepository<T> where T : class
{
    public readonly ILogger _logger = logger;
    protected readonly AppDbContext _context = context;
    internal DbSet<T> _dbSet = context.Set<T>();

    public async Task<bool> Add(T entity)
    {
        await _dbSet.AddAsync(entity);
        return true;
    }

    public async Task<IEnumerable<T>> All()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<bool> Delete(Guid Id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            return false;
        }

        _dbSet.Remove(entity);
        return true;
    }

    public async Task<T?> GetById(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<bool> Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return true;
    }
}
