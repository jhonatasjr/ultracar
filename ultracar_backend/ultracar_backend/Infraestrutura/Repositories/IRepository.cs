using ultracar_backend.Domain.Model;

namespace ultracar_backend.Infraestrutura.Repositories
{
    public interface IRepository
    {

        #region Cliente
        void AdicionarCliente(Cliente cliente);
        List<Cliente> ListarClientes();
        Cliente ObterClientePorId(int id);
        #endregion

        #region Funcionário
        void AdicionarFuncionario(Funcionario funcionario);
        List<Funcionario> ListarFuncionarios();
        #endregion

        #region Produto
        void AdicionarProduto(Produto produto);
        List<Produto> ListarProdutos();
        #endregion Produtos


        #region Ordem de Produção
        void AdicionarOP(OrdemServico ordemServico);
        List<OrdemServico> ListarOP();

        void AdicionarOPItem(OrdemServicoItens ordermServicoItens);
        List<OrdemServicoItens> ListarOPItens();


        #endregion Fim Ordem de Produção

        #region Veiculo
        void AdicionarVeiculo(Veiculo veiculo);
        List<Veiculo> ListarVeiculos();

        #endregion Fim Veiculo
    }
}
