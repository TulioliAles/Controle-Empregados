using ControleEmpregados.Dados;
using ControleEmpregados.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ControleEmpregados.Repository.Classes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected EmpregadoContext context;
        internal DbSet<T> dbSet;
        protected readonly ILogger _logger;

        public GenericRepository(EmpregadoContext context, ILogger logger)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
            this._logger = logger;

        }

        public virtual async Task<IEnumerable<T>> All(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<bool> Delete(Guid id, T entity)
        {
            var exist = dbSet.Contains(entity);
            exist.ToString();

            return exist;
        }

        public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<bool> Upsert(T entity)
        {
            return await dbSet.ContainsAsync(entity);
        }

        public Task<IEnumerable<T>> All()
        {
            throw new NotImplementedException();
        }
    }
}
