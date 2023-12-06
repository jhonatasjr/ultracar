using Microsoft.AspNetCore.Mvc;
using ultracar_backend.Application.ViewModel;
using ultracar_backend.Domain.Model;
using ultracar_backend.Infraestrutura.Repositories;

namespace ultracar_backend.Controllers
{
    [ApiController]
    [Route("api/veiculo")]
    public class VeiculoController : ControllerBase
    {
        private readonly IRepository _repository;

        public VeiculoController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult AdicionarVeiculo([FromForm]VeiculoViewModel veiculo)
        {
            try
            {
                var novoVeiculo = new Veiculo
                {
                    Nome = veiculo.Nome,
                    Placa = veiculo.Placa,
                    Ano = veiculo.Ano,
                    IdCliente = veiculo.IdCliente,
                };

                _repository.AdicionarVeiculo(novoVeiculo);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao adicionar o Veículo: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult ListarVeiculos()
        {
            try
            {
                var lstVeiculos = _repository.ListarVeiculos();
                return Ok(lstVeiculos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao listar os veículos: {ex.Message}");
            }
        }
    }
}
