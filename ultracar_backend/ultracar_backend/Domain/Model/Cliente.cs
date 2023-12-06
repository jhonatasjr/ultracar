using System.ComponentModel.DataAnnotations;

namespace ultracar_backend.Domain.Model
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public int? IdVeiculo { get; set; }
        public List<Veiculo> Veiculos { get; set; }
    }
}
