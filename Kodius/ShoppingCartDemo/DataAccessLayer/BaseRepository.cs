using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCartDemo.DataAccessLayer
{
    public class Repository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly WebShopDbContext _dbcontext;
        private readonly DbSet<T> entities;

        public Repository(WebShopDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            entities = _dbcontext.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default)
        {
            return await entities.ToListAsync(token);
        }

        public T GetById(int id)
        {
            var entity = entities.SingleOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new EntityNotFoundException(id, typeof(T));
            }
            return entity;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await entities.SingleOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new EntityNotFoundException(id, typeof(T));
            }
            return entity;
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
        }

        public int InsertAndGetId(T entity)
        {
            Insert(entity);
            _dbcontext.SaveChanges();
            return entity.Id;
        }

        public async Task<int> InsertAndGetIdAsync(T entity, CancellationToken token = default)
        {
            Insert(entity);
            await _dbcontext.SaveChangesAsync(token);
            return entity.Id;
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _dbcontext.SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await _dbcontext.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var entity = entities.SingleOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new EntityNotFoundException(id, typeof(T));
            }
            entities.Remove(entity);
            _dbcontext.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await entities.SingleOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new EntityNotFoundException(id, typeof(T));
            }
            entities.Remove(entity);
            await _dbcontext.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _dbcontext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbcontext.SaveChangesAsync();
        }

        public IQueryable<T> Get()
        {
            return entities.AsQueryable();
        }
    }
}
