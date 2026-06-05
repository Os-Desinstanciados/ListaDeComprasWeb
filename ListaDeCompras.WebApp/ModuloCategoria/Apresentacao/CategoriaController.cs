using ListaDeCompras.WebApp.ModuloCategoria.Aplicacao;
using ListaDeCompras.WebApp.ModuloCategoria.Dominio;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeCompras.WebApp.ModuloCategoria.Apresentacao;

public class CategoriaController : Controller
{
    private readonly ServicoCategoria servicoCategoria;

    public CategoriaController(ServicoCategoria servicoCategoria)
    {
        this.servicoCategoria = servicoCategoria;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarCategoriasDto> dtos = servicoCategoria.SelecionarTodos();

        List<ListarCategoriasViewModel> listarVms = dtos
            .Select(c => new ListarCategoriasViewModel(c.Id, c.Nome, c.Cor))
            .ToList();

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarCategoriaViewModel cadastrarVm = new CadastrarCategoriaViewModel(
            string.Empty,
            string.Empty
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarCategoriaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarCategoriaDto dto = new CadastrarCategoriaDto(
            cadastrarVm.Nome,
            cadastrarVm.Cor
        );

        Result resultado = servicoCategoria.Cadastrar(dto);

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
        Result<DetalhesCategoriaDto> resultado = servicoCategoria.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = resultado.Errors.First().Message;

            return RedirectToAction(nameof(Listar));
        }

        DetalhesCategoriaDto dto = resultado.Value;

        EditarCategoriaViewModel editarVm = new EditarCategoriaViewModel(
            id,
            dto.Nome,            
            dto.Cor
        );

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarCategoriaViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        Result resultado = servicoCategoria.Editar(new EditarCategoriaDto(
            editarVm.Id,
            editarVm.Nome,
            editarVm.Cor
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
        Result<DetalhesCategoriaDto> resultado = servicoCategoria.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = resultado.Errors.First().Message;

            return RedirectToAction(nameof(Listar));
        }

        DetalhesCategoriaDto dto = resultado.Value;

        ExcluirCategoriaViewModel excluirVm = new ExcluirCategoriaViewModel(
            id,
            dto.Nome,            
            dto.Cor
        );

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirCategoriaViewModel excluirVm)
    {
        Result resultado = servicoCategoria.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData["MensagemErro"] = resultado.Errors.First().Message;

        return RedirectToAction(nameof(Listar));
    }
}
