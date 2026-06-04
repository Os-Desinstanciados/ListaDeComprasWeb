// APS.NET Core
// Builder de um server web
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// Criação de Instancia do servidor Web
WebApplication app = builder.Build();

// Middlewares - Funções que executam a cada chamada
app.UseStaticFiles();
app.UseRouting();
app.MapDefaultControllerRoute();

// Iniciar o looping da aplicação
app.Run();
