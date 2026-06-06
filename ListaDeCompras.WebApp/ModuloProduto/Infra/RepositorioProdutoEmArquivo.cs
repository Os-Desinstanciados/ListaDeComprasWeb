using ListaDeCompras.WebApp.Compartilhado.Infra.Arquivos;
using ListaDeCompras.WebApp.ModuloProduto.Dominio;

namespace ListaDeCompras.WebApp.ModuloProduto.Infra;

public class RepositorioProdutoEmArquivo : RepositorioBaseEmArquivo<Produto>, IRepositorioProduto
{
    public RepositorioProdutoEmArquivo(ContextoJson contexto) : base(contexto) { }

    protected override List<Produto> CarregarRegistros()
    {
        return contexto.Produtos;
    }
}