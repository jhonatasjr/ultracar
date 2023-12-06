using System.ComponentModel.DataAnnotations;

namespace ultracar_backend.Domain.Model
{
    public class Funcionario
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
    }
}
