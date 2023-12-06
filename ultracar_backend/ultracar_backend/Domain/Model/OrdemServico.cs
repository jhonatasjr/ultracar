using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ultracar_backend.Domain.Model
{
    public class OrdemServico
    {
        [Key]
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdFuncionarioResponsavel { get; set; }
        public string DescricaoServico { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValorTotal { get; set; }
        public DateTime DtInicioServico { get; set; }
        public DateTime DtFimServico { get; set; }
    }
}
