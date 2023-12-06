namespace ultracar_backend.Application.ViewModel
{
    public class OrdemServicoViewModel
    {
        public int IdCliente { get; set; }
        public int IdFuncionarioResponsavel { get; set; }
        public string DescricaoServico { get; set; }
        public string DtInicioServico { get; set; }
        public string DtFimServico { get; set; }
        public List<OrdemServicoItensViewModel> OrdemServicoItens { get; set; }
    }
}
