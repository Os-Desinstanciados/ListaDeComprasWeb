using ListaDeCompras.WebApp.Compartilhado;
using ListaDeCompras.WebApp.Compartilhado.Arquivos;
using ListaDeCompras.WebApp.Models;
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

        List<ListarCategoriasViewModel> listarVm = new List<ListarCategoriasViewModel>();

        foreach (Categoria c in categorias)
        {
            ListarCategoriasViewModel viewModel = new ListarCategoriasViewModel(
                c.Id,
                c.Nome,
                c.Cor
            );

            listarVm.Add(viewModel);
        }
        
        return View(listarVm);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarCategoriaViewModel cadastrarVm)
    {
        Categoria novaCategoria = new Categoria(
            cadastrarVm.Nome,
            cadastrarVm.Cor
        );

        repositorioCategoria.Cadastrar(novaCategoria);
        
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(id);

        if (categoria == null)
            return RedirectToAction(nameof(Listar));

        EditarCategoriaViewModel editarVm = new EditarCategoriaViewModel(
            id,
            categoria.Nome,
            categoria.Cor
        );

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarCategoriaViewModel editarVm)
    {
        Categoria categoriaAtualizada = new Categoria(
            editarVm.Nome,
            editarVm.Cor
        );

        repositorioCategoria.Editar(editarVm.Id, categoriaAtualizada);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(id);

        if (categoria == null)
            return RedirectToAction(nameof(Listar));

        ExcluirCategoriaViewModel excluirVm = new ExcluirCategoriaViewModel(
            id,
            categoria.Nome,
            categoria.Cor
        );

        return View(excluirVm);
    }

    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ExcluirConfirmado(ExcluirCategoriaViewModel excluirVm)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(excluirVm.Id);

        if (categoria == null)
            return RedirectToAction(nameof(Listar));

        repositorioCategoria.Excluir(categoria);

        return RedirectToAction(nameof(Listar));
    }
}