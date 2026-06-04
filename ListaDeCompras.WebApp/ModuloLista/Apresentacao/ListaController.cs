using ListaDeCompras.WebApp.ModuloLista.Aplicacao;
using ListaDeCompras.WebApp.ModuloLista.Dominio;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeCompras.WebApp.ModuloLista.Apresentacao;

public class ListaController : Controller
{
    private readonly ServicoLista servicoLista;

    public ListaController(ServicoLista servicoLista) //Erro : Method must have a return type
    {
        this.servicoLista = servicoLista;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarListasDto> dtos = servicoLista.SelecionarTodos();

        List<ListarListasViewModel> listarVms = dtos
            .Select(l => new ListarListasViewModel(l.Id, l.Nome, l.DataCriacao))
            .ToList();

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarListaViewModel cadastrarVm = new CadastrarListaViewModel(
            string.Empty,
            DateTime.Now
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarListaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarListaDto dto = new CadastrarListaDto(
            cadastrarVm.Nome,
            cadastrarVm.DataCriacao
        );

        Result resultado = servicoLista.Cadastrar(dto);

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
        Result<DetalhesListaDto> resultado = servicoLista.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = resultado.Errors.First().Message;

            return RedirectToAction(nameof(Listar));
        }

        DetalhesListaDto dto = resultado.Value;

        EditarListaViewModel editarVm = new EditarListaViewModel(
            id,
            dto.Nome,            
            dto.DataCriacao
        );

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarListaViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        Result resultado = servicoLista.Editar(new EditarListaDto(
            editarVm.Id,
            editarVm.Nome,
            editarVm.DataCriacao
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
        Result<DetalhesListaDto> resultado = servicoLista.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = resultado.Errors.First().Message;

            return RedirectToAction(nameof(Listar));
        }

        DetalhesListaDto dto = resultado.Value;

        ExcluirListaViewModel excluirVm = new ExcluirListaViewModel(
            id,
            dto.Nome,            
            dto.DataCriacao
        );

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirListaViewModel excluirVm)
    {
        Result resultado = servicoLista.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData["MensagemErro"] = resultado.Errors.First().Message;

        return RedirectToAction(nameof(Listar));
    }
}
