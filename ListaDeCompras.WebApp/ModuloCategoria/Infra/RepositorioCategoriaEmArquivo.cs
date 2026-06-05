using ListaDeCompras.WebApp.Compartilhado.Infra.Arquivos;
using ListaDeCompras.WebApp.ModuloCategoria.Dominio;

namespace ListaDeCompras.WebApp.ModuloCategoria.Infra;

public class RepositorioCategoriaEmArquivo : RepositorioBaseEmArquivo<Categoria>, IRepositorioCategoria
{
    public RepositorioCategoriaEmArquivo(ContextoJson contexto) : base(contexto) { }

    protected override List<Categoria> CarregarRegistros()
    {
        return contexto.Categorias;
    }
}