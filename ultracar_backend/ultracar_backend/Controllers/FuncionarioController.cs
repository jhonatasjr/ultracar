using Microsoft.AspNetCore.Mvc;
using ultracar_backend.Application.ViewModel;
using ultracar_backend.Domain.Model;
using ultracar_backend.Infraestrutura.Repositories;

namespace ultracar_backend.Controllers
{
    [ApiController]
    [Route("api/funcionario")]
    public class FuncionarioController : ControllerBase
    {
        private readonly IRepository _repository;

        public FuncionarioController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult AdicionarFuncionario([FromForm]FuncionarioViewModel funcionario)
        {
            try
            {
                var novoFuncionario = new Funcionario
                {
                    Nome = funcionario.Nome,
                    Cargo = funcionario.Cargo,
                };

                _repository.AdicionarFuncionario(novoFuncionario);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao adicionar o Funcionário: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult ListarFuncionarios()
        {
            try
            {
                var lstFuncionarios = _repository.ListarFuncionarios();
                return Ok(lstFuncionarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao listar os funcionários: {ex.Message}");
            }
        }
    }
}
