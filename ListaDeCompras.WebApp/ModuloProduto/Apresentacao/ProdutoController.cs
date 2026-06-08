using ListaDeCompras.WebApp.ModuloProduto.Aplicacao;
using ListaDeCompras.WebApp.ModuloCategoria.Aplicacao;
using ListaDeCompras.WebApp.ModuloProduto.Dominio;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ListaDeCompras.WebApp.ModuloProduto.Apresentacao;

public class ProdutoController : Controller
{
    private readonly ServicoProduto servicoProduto;
    private readonly ServicoCategoria servicoCategoria;

    public ProdutoController(ServicoProduto servicoProduto, ServicoCategoria servicoCategoria)
    {
        this.servicoProduto = servicoProduto;
        this.servicoCategoria = servicoCategoria;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarProdutosDto> dtos = servicoProduto.SelecionarTodos();

        List<ListarProdutosViewModel> listarVms = dtos
            .Select(p => new ListarProdutosViewModel(p.Id, p.Nome, p.CategoriaNome, p.Unidade, p.Preco))
            .ToList();

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarProdutoViewModel cadastrarVm = new CadastrarProdutoViewModel(
            string.Empty,
            string.Empty,
            string.Empty,
            0
        );

        ViewBag.Categorias = ObterMapeamentoCategorias();

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarProdutoViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categorias = ObterMapeamentoCategorias();
            return View(cadastrarVm);
        }

        CadastrarProdutoDto dto = new CadastrarProdutoDto(
            cadastrarVm.Nome,
            cadastrarVm.CategoriaId, 
            cadastrarVm.Unidade,
            cadastrarVm.Preco
        );

        Result resultado = servicoProduto.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            foreach (IError erro in resultado.Errors)
            {
                string campo =
                    erro.Metadata["Campo"] is string ? erro.Metadata["Campo"].ToString()! : string.Empty;

                ModelState.AddModelError(campo, erro.Message);
            }

            ViewBag.Categorias = ObterMapeamentoCategorias();
            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        Result<DetalhesProdutoDto> resultado = servicoProduto.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = resultado.Errors.First().Message;

            return RedirectToAction(nameof(Listar));
        }

        DetalhesProdutoDto dto = resultado.Value;

        EditarProdutoViewModel editarVm = new EditarProdutoViewModel(
            id,
            dto.Nome,            
            dto.CategoriaNome,
            dto.Unidade,
            dto.Preco
        );

        ViewBag.Categorias = ObterMapeamentoCategorias();
        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarProdutoViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        Result resultado = servicoProduto.Editar(new EditarProdutoDto(
            editarVm.Id,
            editarVm.Nome,
            editarVm.CategoriaId,
            editarVm.Unidade,
            editarVm.Preco
        ));

        if (resultado.IsFailed)
        {
            foreach (IError erro in resultado.Errors)
            {
                string campo =
                    erro.Metadata["Campo"] is string ? erro.Metadata["Campo"].ToString()! : string.Empty;

                ModelState.AddModelError(campo, erro.Message);
            }
            ViewBag.Categorias = ObterMapeamentoCategorias();
            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {
        Result<DetalhesProdutoDto> resultado = servicoProduto.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = resultado.Errors.First().Message;

            return RedirectToAction(nameof(Listar));
        }

        DetalhesProdutoDto dto = resultado.Value;

        ExcluirProdutoViewModel excluirVm = new ExcluirProdutoViewModel(
            id,
            dto.Nome,            
            dto.CategoriaNome,
            dto.Unidade,
            dto.Preco
        );

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirProdutoViewModel excluirVm)
    {
        Result resultado = servicoProduto.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData["MensagemErro"] = resultado.Errors.First().Message;

        return RedirectToAction(nameof(Listar));
    }

    private SelectList ObterMapeamentoCategorias()
    {
        // Altere para o método real que você usa para listar as categorias (Ex: selecionar todas)
        var categorias = servicoCategoria.SelecionarTodos(); 
        return new SelectList(categorias, "Id", "Nome");
    }
}
