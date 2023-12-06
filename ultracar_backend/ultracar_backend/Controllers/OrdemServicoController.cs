using Microsoft.AspNetCore.Mvc;
using ultracar_backend.Application.ViewModel;
using ultracar_backend.Domain.Model;
using ultracar_backend.Infraestrutura.Repositories;

namespace ultracar_backend.Controllers
{
    [ApiController]
    [Route("api/ordemServico")]
    public class OrdemServicoController : ControllerBase
    {
        private readonly IRepository _repository;

        public OrdemServicoController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult AdicionarOrdemServico(OrdemServicoViewModel ordemServico)
        {
            try
            {
                var DtInicio = Convert.ToDateTime(ordemServico.DtInicioServico).ToUniversalTime();
                var DtFim = Convert.ToDateTime(ordemServico.DtFimServico).ToUniversalTime();


                var novaOrdemProducao = new OrdemServico
                {
                    IdCliente = ordemServico.IdCliente,
                    IdFuncionarioResponsavel = ordemServico.IdFuncionarioResponsavel,
                    DescricaoServico = ordemServico.DescricaoServico,
                    DtInicioServico = DtInicio,
                    DtFimServico = DtFim,
                };

                _repository.AdicionarOP(novaOrdemProducao);

                foreach (var item in ordemServico.OrdemServicoItens)
                {
                    var novoOrdemProducaoItem = new OrdemServicoItens
                    {
                        IdOrdemServico = novaOrdemProducao.Id,
                        IdProduto = item.IdProduto,
                        Quantidade = item.Quantidade,
                        ValorTotal = item.ValorTotal,
                    };

                    _repository.AdicionarOPItem(novoOrdemProducaoItem);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao criar a Ordem de Serviço Item: {ex.Message}");
            }
        }


        [HttpGet]
        public IActionResult ListarOrdenServicos()
        {
            try
            {
                var lstOP = _repository.ListarOP();
                return Ok(lstOP);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao listar as Ordens de Serviço: {ex.Message}");
            }
        }
    }
}
