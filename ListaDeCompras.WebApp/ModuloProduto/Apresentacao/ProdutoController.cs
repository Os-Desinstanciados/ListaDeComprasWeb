using ListaDeCompras.WebApp.ModuloProduto.Aplicacao;
using ListaDeCompras.WebApp.ModuloProduto.Dominio;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeCompras.WebApp.ModuloProduto.Apresentacao;

public class ProdutoController : Controller
{
    private readonly ServicoProduto servicoProduto;

    public ProdutoController(ServicoProduto servicoProduto)
    {
        this.servicoProduto = servicoProduto;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarProdutosDto> dtos = servicoProduto.SelecionarTodos();

        List<ListarProdutosViewModel> listarVms = dtos
            .Select(p => new ListarProdutosViewModel(p.Id, p.Nome, p.Categoria, p.Unidade, p.Preco))
            .ToList();

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarProdutoViewModel cadastrarVm = new CadastrarProdutoViewModel(
            string.Empty,
            null!,
            string.Empty,
            0
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarProdutoViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarProdutoDto dto = new CadastrarProdutoDto(
            cadastrarVm.Nome,
            cadastrarVm.Categoria,
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
            dto.Categoria,
            dto.Unidade,
            dto.Preco
        );

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
            editarVm.Categoria,
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
            dto.Categoria,
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
}
