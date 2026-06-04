using ListaDeCompras.WebApp.Compartilhado;
using ListaDeCompras.WebApp.Compartilhado.Arquivos;
using ListaDeCompras.WebApp.ModuloCategoria;

namespace ListaDeComprasWeb.ModuloCategoria;

public class RepositorioCategoriaEmArquivo : RepositorioBaseEmArquivo<Categoria>, IRepositorio<Categoria>
{
    public RepositorioCategoriaEmArquivo(ContextoJson contexto) : base(contexto) { }

    protected override List<Categoria> CarregarRegistros()
    {
        return contexto.Categorias;
    }
}