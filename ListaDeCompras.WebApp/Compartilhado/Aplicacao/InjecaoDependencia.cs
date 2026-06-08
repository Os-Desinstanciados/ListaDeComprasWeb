using ListaDeCompras.WebApp.Modulos.ModuloCategoria.Aplicacao;

namespace ListaDeCompras.WebApp.Compartilhado.Aplicacao;

public static class InvecaoDependencia
{
    public static void AddAplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ServicoCategoria>();
    }
}