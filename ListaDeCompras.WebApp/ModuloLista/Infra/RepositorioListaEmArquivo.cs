using ListaDeCompras.WebApp.Compartilhado.Infra.Arquivos;
using ListaDeCompras.WebApp.ModuloLista.Dominio;

namespace ListaDeCompras.WebApp.ModuloLista.Infra;

public class RepositorioListaEmArquivo : RepositorioBaseEmArquivo<Lista>, IRepositorioLista
{
    public RepositorioListaEmArquivo(ContextoJson contexto) : base(contexto) { }

    protected override List<Lista> CarregarRegistros()
    {
        return contexto.Listas;
    }
}
