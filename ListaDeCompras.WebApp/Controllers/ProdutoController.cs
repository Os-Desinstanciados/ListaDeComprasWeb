using ListaDeCompras.WebApp.Compartilhado;
using ListaDeCompras.WebApp.Compartilhado.Arquivos;
using ListaDeCompras.WebApp.ModuloProduto;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeCompras.WebApp.Controllers;

public class ProdutoController : Controller
{
    private readonly IRepositorio<Produto> repositorioProduto;

    public ProdutoController()
    {
        ContextoJson contexto = new ContextoJson();
        contexto.Carregar();

        repositorioProduto = new RepositorioProdutoEmArquivo(contexto);
    }

    public ActionResult Listar()
    {
        List<Produto> produtos = repositorioProduto.SelecionarTodos();

        return View(produtos);
    }
}