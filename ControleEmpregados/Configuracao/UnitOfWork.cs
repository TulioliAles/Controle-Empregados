using ControleEmpregados.Dados;
using ControleEmpregados.Repository.Classes;
using ControleEmpregados.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleEmpregados.Configuracao
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly EmpregadoContext _context;
        private readonly ILogger _logger;

        public IEmpregadoRepository Empregado { get; private set; }

        public UnitOfWork(EmpregadoContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            Empregado = new EmpregadoRepository(context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
