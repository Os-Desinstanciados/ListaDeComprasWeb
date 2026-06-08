using ListaDeCompras.WebApp.Modulos.ModuloCategoria.Dominio;
using ListaDeCompras.WebApp.Modulos.ModuloCategoria.Infra;
using ListaDeCompras.WebApp.Modulos.ModuloProduto.Dominio;
using ListaDeCompras.WebApp.Modulos.ModuloProduto.Infra;

namespace ListaDeCompras.WebApp.Compartilhado.Infra.Arquivos;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(this IServiceCollection services)
    {
        services.AddScoped(provider =>
        {
            ContextoJson contextoJson = new ContextoJson();

            contextoJson.Carregar();

            return contextoJson;
        });

        services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmArquivo>();
        services.AddScoped<IRepositorioProduto, RepositorioProdutoEmArquivo>();
    }
}