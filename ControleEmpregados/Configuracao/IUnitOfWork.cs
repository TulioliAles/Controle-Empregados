using ControleEmpregados.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleEmpregados.Configuracao
{
    public interface IUnitOfWork
    {
        IEmpregadoRepository Empregado { get; }
        Task CompleteAsync();
        void Dispose();
    }
}
