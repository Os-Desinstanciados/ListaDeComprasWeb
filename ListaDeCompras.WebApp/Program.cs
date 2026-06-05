using ListaDeCompras.WebApp.Compartilhado.Infra.Arquivos;
using ListaDeCompras.WebApp.ModuloCategoria.Aplicacao;
using ListaDeCompras.WebApp.ModuloCategoria.Dominio;
using ListaDeCompras.WebApp.ModuloCategoria.Infra;
using ListaDeCompras.WebApp.ModuloLista.Aplicacao;
using ListaDeCompras.WebApp.ModuloLista.Dominio;
using ListaDeCompras.WebApp.ModuloLista.Infra;


var builder = WebApplication.CreateBuilder(args);

#region Configuração de Serviços de Infraestrutura

builder.Services.AddScoped(provider =>
{
    ContextoJson contextoJson = new ContextoJson();

    contextoJson.Carregar();

    return contextoJson;
});

builder.Services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmArquivo>();
builder.Services.AddScoped<IRepositorioLista, RepositorioListaEmArquivo>();

builder.Services.AddScoped<ServicoLista>();
builder.Services.AddScoped<ServicoCategoria>();


#endregion

#region Configuração do MVC

builder.Services.AddControllersWithViews().AddRazorOptions(options =>
{
    // Reseta a configuração padrão do MVC
    options.ViewLocationFormats.Clear();

    // Localização das Views dos módulos: /ModuloCaixa/Apresentacao/Views/Listar.cshtml
    options.ViewLocationFormats.Add("/Modulo{1}/Apresentacao/Views/{0}.cshtml");

    // Localização das Views compartilhadas: /Compartilhado/Apresentacao/Views/_Layout.cshtml
    options.ViewLocationFormats.Add("/Compartilhado/Apresentacao/Views/{0}.cshtml");
});

#endregion

var app = builder.Build();

// Configuração de Middlewares
app.UseStaticFiles();

app.UseRouting();
app.MapDefaultControllerRoute();

// Execução do Servidor
app.Run();
