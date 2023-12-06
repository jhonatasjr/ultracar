using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ultracar_backend.Domain.Model
{
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Placa { get; set; }
        public string Ano { get; set; }
        public int IdCliente { get; set; }

        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; }

    }
}
