using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ultracar_backend.Domain.Model
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Preco { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Quantidade { get; set; }
    }
}
