using ControleEmpregados.Configuracao;
using ControleEmpregados.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleEmpregados.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EmpregadoController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public EmpregadoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _unitOfWork.Empregado.All();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpregado(Guid id)
        {
            var item = await _unitOfWork.Empregado.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmpregado(Empregado empregado)
        {
            if (ModelState.IsValid)
            {
                empregado.Id = Guid.NewGuid();

                await _unitOfWork.Empregado.Add(empregado);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetEmployee", new { empregado.Id }, empregado);
            }

            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpregado(Guid id)
        {
            var item = await _unitOfWork.Empregado.GetById(id);

            if (item == null)
                return BadRequest();

            await _unitOfWork.Empregado.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }
    }
}
