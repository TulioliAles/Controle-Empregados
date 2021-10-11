using ControleEmpregados.Dados;
using ControleEmpregados.Models;
using ControleEmpregados.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleEmpregados.Repository.Classes
{
    public class EmpregadoRepository : GenericRepository<Empregado>, IEmpregadoRepository
    {
        public EmpregadoRepository(EmpregadoContext context, ILogger logger) : base(context, logger)
        {
        }

        public virtual async Task<IEnumerable<Empregado>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(EmpregadoRepository));
                return new List<Empregado>();
            }
        }
        public override async Task<bool> Upsert(Empregado entity)
        {
            try
            {
                var existingUser = await dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingUser == null)
                    return await Add(entity);

                existingUser.Nome = entity.Nome;
                existingUser.Sobrenome = entity.Sobrenome;
                existingUser.Email = entity.Email;
                existingUser.Telefone = entity.Telefone;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(EmpregadoRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await dbSet.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                dbSet.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(EmpregadoRepository));
                return false;
            }
        }
    }
}
