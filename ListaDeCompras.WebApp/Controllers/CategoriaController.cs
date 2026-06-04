using ListaDeCompras.WebApp.Compartilhado;
using ListaDeCompras.WebApp.Compartilhado.Arquivos;
using ListaDeCompras.WebApp.ModuloCategoria;
using ListaDeComprasWeb.ModuloCategoria;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeCompras.WebApp.Controllers;

public class CategoriaController : Controller
{
    private readonly IRepositorio<Categoria> repositorioCategoria;

    public CategoriaController()
    {
        ContextoJson contexto = new ContextoJson();
        contexto.Carregar();

        repositorioCategoria = new RepositorioCategoriaEmArquivo(contexto);
    }

    // GET: CategoriaController
    public ActionResult Listar()
    {
        List<Categoria> categorias = repositorioCategoria.SelecionarTodos();


        return View(categorias);
    }

    public ActionResult Cadastrar()
    {
        return View();
    }
}