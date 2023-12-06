using Microsoft.AspNetCore.Mvc;
using ultracar_backend.Application.ViewModel;
using ultracar_backend.Domain.Model;
using ultracar_backend.Infraestrutura.Repositories;

namespace ultracar_backend.Controllers
{
    [ApiController]
    [Route("api/produto")]
    public class ProdutoController : ControllerBase
    {

        private readonly IRepository _repository;

        public ProdutoController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult AdicionarProduto([FromForm]ProdutoViewModel produto)
        {
            try
            {
                var novoProduto = new Produto
                {
                    Nome = produto.Nome,
                    Marca = produto.Marca,
                    Preco = produto.Preco,
                    Quantidade = produto.Quantidade,
                };

                _repository.AdicionarProduto(novoProduto);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao adicionar o Funcionário: {ex.Message}");
            }
        }


        [HttpGet]
        public IActionResult ListarProdutos()
        {
            try
            {
                var lstProdutos = _repository.ListarProdutos();
                return Ok(lstProdutos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao listar os funcionários: {ex.Message}");
            }
        }
    }
}
