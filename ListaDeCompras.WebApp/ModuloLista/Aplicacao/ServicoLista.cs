using ListaDeCompras.WebApp.ModuloLista.Dominio;
using FluentResults;

namespace ListaDeCompras.WebApp.ModuloLista.Aplicacao;

public class ServicoLista
{
    private readonly IRepositorioLista repositorioLista;    

    public ServicoLista(
        IRepositorioLista repositorioLista
        
    )
    {
        this.repositorioLista = repositorioLista;       
    }

    public Result Cadastrar(CadastrarListaDto dto)
    {
        if (ExisteListaComNome(dto.Nome))
            return Falha("Etiqueta", "Já existe uma caixa com esta etiqueta.");

        Lista novaLista = new Lista(
            dto.Nome,
            dto.DataCriacao
        );

        repositorioLista.Cadastrar(novaLista);

        return Result.Ok();
    }

    public Result Editar(EditarListaDto dto)
    {
        if (ExisteListaComNome(dto.Nome, dto.Id))
            return Falha("Etiqueta", "Já existe uma caixa com esta etiqueta.");

        Lista listaAtualizada = new Lista(dto.Nome, dto.DataCriacao);

        bool conseguiuEditar = repositorioLista.Editar(dto.Id, listaAtualizada);

        if (!conseguiuEditar)
            return Result.Fail("Caixa não encontrada.");

        return Result.Ok();
    }

    public Result Excluir(string id)
    {
        Lista? lista = repositorioLista.SelecionarPorId(id);

        if (lista == null)
            return Result.Fail("Lista não encontrada.");
        
        repositorioLista.Excluir(id);

        return Result.Ok();
    }

    public List<ListarListasDto> SelecionarTodos()
    {
        List<Lista> listas = repositorioLista.SelecionarTodos();

        return listas
            .Select(c => new ListarListasDto(c.Id, c.Nome, c.DataCriacao))
            .ToList();
    }

    public Result<DetalhesListaDto> SelecionarPorId(string id)
    {
        Lista? lista = repositorioLista.SelecionarPorId(id);

        if (lista == null)
            return Result.Fail("Caixa não encontrada.");

        return Result.Ok(new DetalhesListaDto(lista.Id, lista.Nome, lista.DataCriacao));
    }

    private bool ExisteListaComNome(string nome, string? idIgnorado = null)
    {
        List<Lista> caixas = repositorioLista.SelecionarTodos();

        foreach (Lista l in caixas)
        {
            if (l.Id != idIgnorado && string.Equals(l.Nome, nome, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    private static Result Falha(string campo, string mensagem)
    {
        IError erro = new Error(mensagem).WithMetadata("Campo", campo);

        return Result.Fail(erro);
    }
}
