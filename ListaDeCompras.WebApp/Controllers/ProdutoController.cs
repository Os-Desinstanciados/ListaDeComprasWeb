using ListaDeCompras.WebApp.Compartilhado;
using ListaDeCompras.WebApp.Compartilhado.Arquivos;
using ListaDeCompras.WebApp.Models;
using ListaDeCompras.WebApp.ModuloCategoria;
using ListaDeCompras.WebApp.ModuloProduto;
using ListaDeComprasWeb.ModuloCategoria;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


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

        CadastrarProdutoViewModel cadastrarVm = new(
            "",
            "",
            "",
            0.01m
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarProdutoViewModel cadastrarVm)
    {
        ViewBag.Categorias = CarregarCategorias();

        Categoria? categoria = repositorioCategoria.SelecionarPorId(cadastrarVm.CategoriaId);

        if (!string.IsNullOrWhiteSpace(cadastrarVm.CategoriaId) && categoria == null)
        {
            ModelState.AddModelError(
                nameof(cadastrarVm.CategoriaId),
                "Selecione uma categoria válida."
            );
        }

        if (!ModelState.IsValid)
            return View(cadastrarVm);

        Produto novoProduto = new Produto(
            cadastrarVm.Nome,
            categoria!,
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

        ViewBag.Categorias = CarregarCategorias();

        EditarProdutoViewModel editarVm = new(
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
        ViewBag.Categorias = CarregarCategorias();

        Categoria? categoriaSelecionada =
            repositorioCategoria.SelecionarPorId(editarVm.CategoriaId);

        if (!string.IsNullOrWhiteSpace(editarVm.CategoriaId) && categoriaSelecionada == null)
        {
            ModelState.AddModelError(
                nameof(editarVm.CategoriaId),
                "Selecione uma categoria válida."
            );
        }

        if (!ModelState.IsValid)
            return View(editarVm);

        Produto produtoAtualizado = new Produto(
            editarVm.Nome,
            categoriaSelecionada!,
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

    private List<SelectListItem> CarregarCategorias()
    {
        List<Categoria> categorias = repositorioCategoria.SelecionarTodos();

        List<SelectListItem> categoriasVm = new();

        foreach (Categoria c in categorias)
        {
            SelectListItem item = new(
                c.Nome,
                c.Id
            );

            categoriasVm.Add(item);
        }

        return categoriasVm;
    }
}