using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCartDemo.DataAccessLayer
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        void Delete(int id);
        Task DeleteAsync(int id);
        IQueryable<T> Get();
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default);
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        void Insert(T entity);
        int InsertAndGetId(T entity);
        Task<int> InsertAndGetIdAsync(T entity, CancellationToken token = default);
        void SaveChanges();
        Task SaveChangesAsync();
        void Update(T entity);
        Task UpdateAsync(T entity);
    }
}