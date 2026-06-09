using FluentResults;
using ListaDeCompras.WebApp.Modulos.ModuloCategoria.Dominio;
using ListaDeCompras.WebApp.Modulos.ModuloProduto.Dominio;
using ListaDeCompras.WebApp.Modulos.ModuloCategoria.Aplicacao;

namespace ListaDeCompras.WebApp.Modulos.ModuloCategoria.Aplicacao;

public class ServicoCategoria
{
    private readonly IRepositorioCategoria repositorioCategoria;
    private readonly IRepositorioProduto repositorioProduto;

    public ServicoCategoria(
        IRepositorioCategoria repositorioCategoria,
        IRepositorioProduto repositorioProduto
    )
    {
        this.repositorioCategoria = repositorioCategoria;
        this.repositorioProduto = repositorioProduto;
    }

    public Result Cadastrar(CadastrarCategoriaDto dto)
    {
        if (ExisteCategoriaComNome(dto.Nome))
            return Falha(nameof(dto.Nome), "Já existe uma categoria com este nome.");

        Categoria novaCategoria = new Categoria(dto.Nome, dto.Cor);

        Result resultadoValidacao = ValidarEntidade(novaCategoria);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioCategoria.Cadastrar(novaCategoria);

        return Result.Ok();
    }

    public Result Editar(EditarCategoriaDto dto)
    {
        if (ExisteCategoriaComNome(dto.Nome, dto.Id))
            return Falha(nameof(dto.Nome), "Já existe uma categoria com este nome.");

        Categoria categoriaAtualizada = new Categoria(dto.Nome, dto.Cor);

        Result resultadoValidacao = ValidarEntidade(categoriaAtualizada);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioCategoria.Editar(dto.Id, categoriaAtualizada);

        if (!conseguiuEditar)
            return Result.Fail("Categoria não encontrada.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(id);

        if (categoria == null)
            return Result.Fail("Categoria não encontrada.");

        bool possuiProdutos = repositorioProduto
            .SelecionarTodos()
            .Any(p => p.Categoria.Id == id);

        if (possuiProdutos)
            return Result.Fail("Esta categoria não pode ser excluída pois possui produtos vinculados.");

        repositorioCategoria.Excluir(id);

        return Result.Ok();
    }

    public List<ListarCategoriasDto> SelecionarTodos()
    {
        return repositorioCategoria
            .SelecionarTodos()
            .Select(c => new ListarCategoriasDto(c.Id, c.Nome, c.Cor))
            .ToList();
    }

    public Result<DetalhesCategoriaDto> SelecionarPorId(Guid id)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(id);

        if (categoria == null)
            return Result.Fail("Categoria não encontrada.");

        return Result.Ok(new DetalhesCategoriaDto(categoria.Id, categoria.Nome, categoria.Cor));
    }

    private bool ExisteCategoriaComNome(string nome, Guid? idIgnorado = null)
    {
        return repositorioCategoria
            .SelecionarTodos()
            .Any(c =>
                c.Id != idIgnorado &&
                string.Equals(c.Nome, nome, StringComparison.OrdinalIgnoreCase)
            );
    }

    private static Result ValidarEntidade(Categoria categoria)
    {
        List<string> erros = categoria.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
}