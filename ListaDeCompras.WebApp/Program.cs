// APS.NET Core

// Builder de um server web
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

WebApplication app = builder.Build();

// Middlewares
app.UseRouting();
app.MapDefaultControllerRoute();

// Iniciar o looping da aplicação
app.Run();

