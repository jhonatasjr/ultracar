using System.ComponentModel.DataAnnotations.Schema;

namespace ultracar_backend.Application.ViewModel
{
    public class OrdemServicoItensViewModel
    {
        public int? IdOrdemServico { get; set; }
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValorTotal { get; set; }
    }
}
