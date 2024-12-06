namespace FormulaOne.DataService.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> All();
    Task<T?> GetById(Guid Id);
    Task<bool> Add(T entity);
    Task<bool> Update(T entity);
    Task<bool> Delete(Guid Id);
}
