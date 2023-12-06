using Microsoft.AspNetCore.Mvc;
using ultracar_backend.Application.ViewModel;
using ultracar_backend.Domain.Model;
using ultracar_backend.Infraestrutura.Repositories;

namespace ultracar_backend.Controllers
{
    [ApiController]
    [Route("api/cliente")]
    public class ClienteController : ControllerBase
    {
        private readonly IRepository _repository;

        //Construtor
        public ClienteController(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost]
        public IActionResult AdicionarCliente([FromForm]ClienteViewModel cliente)
        {
            try
            {
                var novoCliente = new Cliente
                {
                    Nome = cliente.Nome,
                    CPF = cliente.CPF,
                    Email = cliente.Email,
                    Telefone = cliente.Telefone,
                    Endereco = cliente.Endereco,
                    IdVeiculo = cliente.IdVeiculo
                };

                _repository.AdicionarCliente(novoCliente);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao adicionar o cliente: {ex.Message}");
            }
        }


        [HttpGet]
        public IActionResult ListarClientes()
        {
            try
            {
                var lstClientes = _repository.ListarClientes();
                return Ok(lstClientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao listar os clientes: {ex.Message}");
            }
        }

        [HttpGet("buscarClientePorId/{id}")]
        public IActionResult BuscarClientePorId(int id)
        {
            try
            {
                var cliente = _repository.ObterClientePorId(id);

                if (cliente != null)
                {
                    return Ok(cliente);
                }
                else
                {
                    return StatusCode(500, $"Não existe cliente com o código fornecido.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao buscar os dados do cliente: {ex.Message}");
            }
        }

    }
}
