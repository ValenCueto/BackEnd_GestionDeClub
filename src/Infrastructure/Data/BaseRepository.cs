using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;


        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity) 
        { 
            _dbContext.Set<T>.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>.Delete(entity);
            _dbContext.SaveChanges();
        }
        public virtual T? GetById<TId>(TId id)
        {
            return _dbContext.Set<T>().Find(new object[] { id });
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            _dbContext.SaveChanges();
        }

        public virtual List<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }
    }
}
