import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Link, useParams } from 'react-router-dom';
import '../componentes/DetalhesCliente.css';

function DetalhesCliente() {
  const { id } = useParams();

  const [cliente, setCliente] = useState(null);
  const [erro, setErro] = useState('');
  const [funcionarios, setFuncionarios] = useState([]);
  const [servicoDescricao, setServicoDescricao] = useState('');
  const [veiculoSelecionado, setVeiculoSelecionado] = useState(null);
  const [produtos, setProdutos] = useState([]);


  useEffect(() => {
    const fetchCliente = async () => {
      try {
        const response = await axios.get(`https://localhost:7267/api/cliente/buscarClientePorId/${id}`);
        setCliente(response.data);
        setErro('');
      } catch (error) {
        console.error('Erro ao buscar cliente:', error);
        setCliente(null);
        setErro('Cliente não encontrado');
      }
    };

    fetchCliente();

    // Carregar funcionários da API
    const fetchFuncionarios = async () => {
      try {
        const response = await axios.get('https://localhost:7267/api/funcionario');
        setFuncionarios(response.data);
      } catch (error) {
        console.error('Erro ao buscar funcionários:', error);
      }
    };

    fetchFuncionarios();

    const fetchProdutos = async () => {
      try {
        const response = await axios.get('https://localhost:7267/api/produto');
        setProdutos(response.data.$values);
      } catch (error) {
        console.error('Erro ao buscar produtos:', error);
      }
    };
    fetchProdutos();


  }, [id]);

  const handleSelecionar = (veiculoId) => {
    const updatedVeiculos = cliente.veiculos.$values.map((veiculo) => ({
      ...veiculo,
      selecionado: veiculo.id === veiculoId ? !veiculo.selecionado : false,
    }));
    setCliente({ ...cliente, veiculos: { $values: updatedVeiculos } });

    // Atualizar estado do veículo selecionado
    const veiculo = cliente.veiculos.$values.find((v) => v.id === veiculoId);
    setVeiculoSelecionado(veiculo);
  };

  const handleServicoDescricaoChange = (e) => {
    setServicoDescricao(e.target.value);
  };

  const handleSelecionarProduto = (produtoId) => {
    const updatedProdutos = produtos.map((produto) =>
      produto.id === produtoId ? { ...produto, selecionado: !produto.selecionado } : produto
    );
    setProdutos(updatedProdutos);
  };

  const getTotalSelecionado = () => {
    let total = 0;
    produtos.forEach((produto) => {
      if (produto.selecionado) {
        total += produto.valorTotal || 0;
      }
    });
    return total.toFixed(2);
  };

  const salvarOrdemServico = async () => {
    const DtFimServico = formatDate(document.getElementById('dataFim').value);
    const mecanicoSelecionado = document.getElementById('funcionariosDropdown').value;
    const valorTotalOrdemServico = getTotalSelecionado();

    const produtosSelecionados = produtos
      .filter((produto) => produto.selecionado && produto.quantidadeSelecionada > 0 && produto.valorTotal > 0);

    if (DtFimServico && mecanicoSelecionado && produtosSelecionados.length > 0) {
      
      const ordemServico = {
        IdCliente: cliente.id,
        DtInicioServico: new Date().toLocaleString(),
        DtFimServico,
        IdFuncionarioResponsavel: mecanicoSelecionado,
        DescricaoServico: servicoDescricao,
        valorTotalOrdemServico,
        OrdemServicoItens: produtosSelecionados.map((produto) => ({
          IdProduto: produto.id,
          Quantidade: produto.quantidadeSelecionada,
          ValorTotal: produto.valorTotal,
        })),
      };

      try {
        const response = await axios.post('https://localhost:7267/api/ordemServico', ordemServico);

        console.log('Ordem de serviço salva:', response.data);
      } catch (error) {
        console.error('Erro ao salvar ordem de serviço:', error);
      }
    } else {
      alert('Preencha todos os campos obrigatórios antes de salvar.');
    }
  };

  const formatDate = (date) => {
    const options = {
      year: 'numeric',
      month: '2-digit',
      day: '2-digit',
      hour: '2-digit',
      minute: '2-digit',
      second: '2-digit',
    };
  
    return new Date(date).toLocaleString('pt-BR', options).replace(',', '');
  };


  if (!cliente) {
    return <p>Carregando...</p>;
  }

  return (
    <div className="detalhes-cliente">
      <h2>Detalhes do Cliente</h2>
      <div className="info-cliente">
        <p><strong>Código:</strong> {cliente.id}</p>
        <p><strong>Nome:</strong> {cliente.nome}</p>
        <p><strong>CPF:</strong> {cliente.cpf}</p>
        <p><strong>Email:</strong> {cliente.email}</p>
        <p><strong>Telefone:</strong> {cliente.telefone}</p>
        <p><strong>Endereço:</strong> {cliente.endereco}</p>
      </div>

      <h3>Veículos</h3>
      <table className="tabela-veiculos">
        <thead>
          <tr>
            <th>Nome</th>
            <th>Placa</th>
            <th>Ano</th>
            <th>Selecionar</th>
          </tr>
        </thead>
        <tbody>
          {cliente.veiculos && cliente.veiculos.$values.map((veiculo) => (
            <tr key={veiculo.id}>
              <td>{veiculo.nome}</td>
              <td>{veiculo.placa}</td>
              <td>{veiculo.ano}</td>
              <td>
                <button
                  disabled={veiculo.selecionado}
                  onClick={() => handleSelecionar(veiculo.id)}
                >
                  {veiculo.selecionado ? 'Selecionado' : 'Selecionar'}
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {veiculoSelecionado && (
        <div className="selecionar-servico">
          <label htmlFor="funcionariosDropdown">Mecânico:</label>
          <select id="funcionariosDropdown">
            {funcionarios && funcionarios.$values.map((funcionario) => (
              <option key={funcionario.id} value={funcionario.id}>{funcionario.nome}</option>
            ))}
          </select>

          <label htmlFor="descricaoServico">Descrição do Serviço:</label>
          <textarea
            id="descricaoServico"
            value={servicoDescricao}
            onChange={handleServicoDescricaoChange}
          ></textarea>

          <div className="valor-total-ordem-producao">
            <h3>Valor Total Ordem Produção:</h3>
            <p>R$ {getTotalSelecionado()}</p>
          </div>

        </div>


      )}

      {veiculoSelecionado && (
        <div className="produtos-disponiveis">
          <h3>Produtos Disponíveis</h3>
          <table className="tabela-produtos">
            <thead>
              <tr>
                <th>Nome</th>
                <th>Qtd Estoque</th>
                <th>Preço Unitário</th>
                <th>Quantidade</th>
                <th>Valor Total</th>
                <th>Selecionar</th>
              </tr>
            </thead>
            <tbody>
              {produtos.map((produto) => (
                <tr
                  key={produto.id}
                  className={produto.selecionado ? 'selecionado' : ''}
                  onClick={() => handleSelecionarProduto(produto.id)}
                >
                  <td>{produto.nome}</td>
                  <td>{produto.quantidade}</td>
                  <td>R$ {produto.preco}</td>
                  <td>
                    <input
                      type="number"
                      min="0"
                      max={produto.quantidade}
                      value={produto.quantidadeSelecionada || 0}
                      onChange={(e) => {
                        const valor = parseInt(e.target.value);
                        const quantidadeSelecionada = valor > produto.quantidade ? produto.quantidade : valor;
                        const valorTotal = quantidadeSelecionada * produto.preco;
                        const updatedProdutos = produtos.map((p) =>
                          p.id === produto.id
                            ? {
                              ...p,
                              quantidadeSelecionada,
                              valorTotal,
                            }
                            : p
                        );
                        setProdutos(updatedProdutos);
                      }}
                    />
                  </td>
                  <td>R$ {(produto.valorTotal || 0).toFixed(2)}</td>
                  <td>
                    <button>Selecionar</button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>

          <div className="datas-servico">
            <div className="data-servico">
              <label htmlFor="dataInicio">Data e Hora Início Serviço:</label>
              <input type="text" id="dataInicio" disabled value={new Date().toLocaleString()} />
            </div>

            <div className="data-servico">
              <label htmlFor="dataFim">Data e Hora Fim Serviço:</label>
              <input type="datetime-local" id="dataFim" />
            </div>
          </div>

        </div>


      )}
      <button onClick={salvarOrdemServico}>Salvar</button>

      <Link to="/" className="link-voltar">Voltar</Link>
    </div>
  );
}

export default DetalhesCliente;
