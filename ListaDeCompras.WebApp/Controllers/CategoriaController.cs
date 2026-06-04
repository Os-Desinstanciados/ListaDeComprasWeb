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
    [HttpGet]
    public ActionResult Listar()
    {
        List<Categoria> categorias = repositorioCategoria.SelecionarTodos();


        return View(categorias);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(string nome, string cor)
    {
        Categoria novaCategoria = new Categoria(nome, cor);

        repositorioCategoria.Cadastrar(novaCategoria);
        
        string listarStr = nameof(Listar);
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(id);

        if (categoria == null)
            return RedirectToAction(nameof(Listar));

        return View(categoria);
    }

    [HttpPost]
    public ActionResult Editar(string id, string nome, string cor)
    {
        Categoria categoriaAtualizada = new Categoria(nome, cor);

        repositorioCategoria.Editar(id, categoriaAtualizada);

        return RedirectToAction(nameof(Listar));

    }
}