using ListaDeCompras.WebApp.Compartilhado.Infra.Arquivos;
using ListaDeCompras.WebApp.Modulos.ModuloProduto.Dominio;

namespace ListaDeCompras.WebApp.Modulos.ModuloProduto.Infra;

public class RepositorioProdutoEmArquivo : RepositorioBaseEmArquivo<Produto>, IRepositorioProduto
{
    public RepositorioProdutoEmArquivo(ContextoJson contexto) : base(contexto) { }

    protected override List<Produto> CarregarRegistros()
    {
        return contexto.Produtos;
    }
}