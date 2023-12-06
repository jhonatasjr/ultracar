using Microsoft.EntityFrameworkCore;
using ultracar_backend.Domain.Model;

namespace ultracar_backend.Infraestrutura
{
    public class Contexto : DbContext
    {
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<OrdemServico> OrdemServico { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Veiculo> Veiculo { get; set; }
        public DbSet<OrdemServicoItens> OrdemServicoItens { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=ultracar;User Id=postgres;Password=ultra;");
            }
            base.OnConfiguring(optionsBuilder);
        }

    }
}
