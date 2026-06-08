using ListaDeCompras.WebApp.Modulos.ModuloCategoria.Aplicacao;
using ListaDeCompras.WebApp.Modulos.ModuloProduto.Aplicacao;

namespace ListaDeCompras.WebApp.Compartilhado.Aplicacao;

public static class InvecaoDependencia
{
    public static void AddAplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ServicoCategoria>();
        services.AddScoped<ServicoProduto>();
    }
}