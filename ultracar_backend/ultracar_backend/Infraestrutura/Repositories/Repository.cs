using Microsoft.EntityFrameworkCore;
using ultracar_backend.Domain.Model;

namespace ultracar_backend.Infraestrutura.Repositories
{
    public class Repository : IRepository
    {
        private readonly Contexto ctx = new Contexto();


        #region Área Cliente

        public void AdicionarCliente(Cliente cliente)
        {
            ctx.Cliente.Add(cliente);
            ctx.SaveChanges();
        }

        public List<Cliente> ListarClientes()
        {
            return ctx.Cliente.Include(cliente => cliente.Veiculos).ToList();
        }

        public Cliente ObterClientePorId(int id)
        {
            return ctx.Cliente.Include(cliente => cliente.Veiculos).FirstOrDefault(c => c.Id == id);
        }

        #endregion Fim Área Cliente

        #region Área Funcionário

        public void AdicionarFuncionario(Funcionario funcionario)
        {
            ctx.Funcionario.Add(funcionario);
            ctx.SaveChanges();
        }
        public List<Funcionario> ListarFuncionarios()
        {
            return ctx.Funcionario.ToList();
        }
        #endregion Funcionário

        #region Produto

        public void AdicionarProduto(Produto produto)
        {
            ctx.Produto.Add(produto);
            ctx.SaveChanges();
        }

        public List<Produto> ListarProdutos()
        {
            return ctx.Produto.ToList();
        }
        #endregion Produtos

        #region Veículo
        public void AdicionarVeiculo(Veiculo veiculo)
        {
            ctx.Veiculo.Add(veiculo);
            ctx.SaveChanges();
        }

        public List<Veiculo> ListarVeiculos()
        {
            return ctx.Veiculo.ToList();
        }
        #endregion


        public void AdicionarOP(OrdemServico ordemServico)
        {
            ctx.OrdemServico.Add(ordemServico);
            ctx.SaveChanges();
        }

        public List<OrdemServico> ListarOP()
        {
            return ctx.OrdemServico.ToList();
        }

        public void AdicionarOPItem(OrdemServicoItens ordermServicoItem)
        {
            ctx.OrdemServicoItens.Add(ordermServicoItem);
            ctx.SaveChanges();
        }

        public List<OrdemServicoItens> ListarOPItens()
        {
            return ctx.OrdemServicoItens.ToList();
        }
    }
}
