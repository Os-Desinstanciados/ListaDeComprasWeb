using ListaDeCompras.WebApp.Compartilhado;
using ListaDeCompras.WebApp.Compartilhado.Arquivos;
using ListaDeCompras.WebApp.Models;
using ListaDeCompras.WebApp.ModuloCategoria;
using ListaDeCompras.WebApp.ModuloProduto;
using ListaDeComprasWeb.ModuloCategoria;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeCompras.WebApp.Controllers;

public class ProdutoController : Controller
{
    private readonly IRepositorio<Produto> repositorioProduto;
    private readonly IRepositorio<Categoria> repositorioCategoria;

    public ProdutoController()
    {
        ContextoJson contexto = new ContextoJson();
        contexto.Carregar();

        repositorioProduto = new RepositorioProdutoEmArquivo(contexto);
        repositorioCategoria = new RepositorioCategoriaEmArquivo(contexto);
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Produto> produtos = repositorioProduto.SelecionarTodos();

        List<ListarProdutoViewModel> listarVm = new List<ListarProdutoViewModel>();

        foreach (Produto p in produtos)
        {
            ListarProdutoViewModel viewModel = new ListarProdutoViewModel(
                p.Id,
                p.Nome,
                p.Categoria.Nome,
                p.UnidadeMedida,
                p.PrecoAproximado
            );

            listarVm.Add(viewModel);
        }

        return View(listarVm);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        ViewBag.Categorias = CarregarCategorias();
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarProdutoViewModel cadastrarVm)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(cadastrarVm.CategoriaId);

        if (categoria == null)
            return RedirectToAction(nameof(Listar));

        Produto novoProduto = new Produto(
            cadastrarVm.Nome,
            categoria,
            cadastrarVm.UnidadeMedida,
            cadastrarVm.PrecoAproximado
        );

        repositorioProduto.Cadastrar(novoProduto);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        Produto? produto = repositorioProduto.SelecionarPorId(id);

        if (produto == null)
            return RedirectToAction(nameof(Listar));

        List<Categoria> categorias = repositorioCategoria.SelecionarTodos();

        List<ListarCategoriasViewModel> categoriasVm = new();

        foreach (Categoria c in categorias)
        {
            categoriasVm.Add(new ListarCategoriasViewModel(
                c.Id,
                c.Nome,
                c.Cor
            ));
        }

        ViewBag.Categorias = categoriasVm;

        EditarProdutoViewModel editarVm = new EditarProdutoViewModel(
            produto.Id,
            produto.Nome,
            produto.Categoria.Id,
            produto.UnidadeMedida,
            produto.PrecoAproximado
        );

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarProdutoViewModel editarVm)
    {
        Categoria? categoriaSelecionada =
            repositorioCategoria.SelecionarPorId(editarVm.CategoriaId);

        if (categoriaSelecionada == null)
            return RedirectToAction(nameof(Listar));

        Produto produtoAtualizado = new Produto(
            editarVm.Nome,
            categoriaSelecionada,
            editarVm.UnidadeMedida,
            editarVm.PrecoAproximado
        );

        repositorioProduto.Editar(editarVm.Id, produtoAtualizado);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {
        Produto? produto = repositorioProduto.SelecionarPorId(id);

        if (produto == null)
            return RedirectToAction(nameof(Listar));

        ExcluirProdutoViewModel excluirVm = new ExcluirProdutoViewModel(
            produto.Id,
            produto.Nome,
            produto.Categoria.Nome,
            produto.UnidadeMedida,
            produto.PrecoAproximado
        );

        return View(excluirVm);
    }

    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ExcluirConfirmado(ExcluirProdutoViewModel excluirVm)
    {
        Produto? produto = repositorioProduto.SelecionarPorId(excluirVm.Id);

        if (produto == null)
            return RedirectToAction(nameof(Listar));

        repositorioProduto.Excluir(produto);

        return RedirectToAction(nameof(Listar));
    }

    private List<ListarCategoriasViewModel> CarregarCategorias()
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

        return listarVm;
    }
}