using ListaDeCompras.WebApp.Compartilhado.Aplicacao;
using ListaDeCompras.WebApp.Compartilhado.Apresentacao;
using ListaDeCompras.WebApp.Compartilhado.Infra.Arquivos;

var builder = WebApplication.CreateBuilder(args);

// Configuração de Dependências (Dependency Injection)
builder.Services.AddInfraRepositories();

builder.Services.AddAplicationServices();

builder.Services.AddPresentationConfig();

var app = builder.Build();

// Configuração de Middlewares
app.UseStaticFiles();

app.UseRouting();
app.MapDefaultControllerRoute();

// Execução do Servidor
app.Run();