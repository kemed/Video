using System.Collections.Generic;
using System.Linq;
using Core.Object;
using System.Threading.Tasks;

namespace Data
{
    public partial interface IRepository<T> where T : class, new()
    {
        ObjectResult<T> Add(T entity);
        List<ObjectResult<T>> Adds(List<T> entity);
        Task<ObjectResult<T>> AddAsync(T entity);
        Task<List<ObjectResult<T>>> AddsAsync(List<T> entity);
        ServiceResult Update(T entity);
        List<ObjectResult<T>> Updates(List<T> entity);
        Task<ServiceResult> UpdateAsync(T entity);
        Task<List<ObjectResult<T>>> UpdatesAsync(List<T> entity);
        ServiceResult Remove(T entity);
        List<ObjectResult<T>> Removes(List<T> entity);
        Task<ServiceResult> RemoveAsync(T entity);
        Task<List<ObjectResult<T>>> RemovesAsync(List<T> entity);
        Task<List<T>> GetListAsync(IQueryable<T> query);
        Task<T> GetDataAsync(IQueryable<T> query);
        Task<int> GetCountAsync(IQueryable<T> query);
        IQueryable<T> GetTable { get; }
    }
}
