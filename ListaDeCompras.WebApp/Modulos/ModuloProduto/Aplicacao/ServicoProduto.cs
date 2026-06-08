using FluentResults;
using ListaDeCompras.WebApp.Modulos.ModuloCategoria.Dominio;
using ListaDeCompras.WebApp.Modulos.ModuloProduto.Dominio;
using ListaDeCompras.WebApp.Modulos.ModuloProduto.Aplicacao;

namespace ListaDeCompras.WebApp.Modulos.ModuloProduto.Aplicacao;

public class ServicoProduto
{
    private readonly IRepositorioProduto repositorioProduto;
    private readonly IRepositorioCategoria repositorioCategoria;

    public ServicoProduto(
        IRepositorioProduto repositorioProduto,
        IRepositorioCategoria repositorioCategoria
    )
    {
        this.repositorioProduto = repositorioProduto;
        this.repositorioCategoria = repositorioCategoria;
    }

    public Result Cadastrar(CadastrarProdutoDto dto)
    {
        Categoria? categoriaSelecionada = repositorioCategoria.SelecionarPorId(dto.CategoriaId);

        if (categoriaSelecionada == null)
            return Falha(nameof(dto.CategoriaId), "Selecione uma categoria válida.");

        if (ExisteProdutoComMesmoNomeNaCategoria(dto.Nome, dto.CategoriaId))
            return Falha(nameof(dto.Nome), "Já existe um produto com este nome nesta categoria.");

        Produto novoProduto = new Produto(
            dto.Nome,
            categoriaSelecionada,
            dto.UnidadeMedida,
            dto.PrecoAproximado
        );

        Result resultadoValidacao = ValidarEntidade(novoProduto);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioProduto.Cadastrar(novoProduto);

        return Result.Ok();
    }

    public Result Editar(EditarProdutoDto dto)
    {
        Produto? produto = repositorioProduto.SelecionarPorId(dto.Id);

        if (produto == null)
            return Result.Fail("Produto não encontrado.");

        Categoria? categoriaSelecionada = repositorioCategoria.SelecionarPorId(dto.CategoriaId);

        if (categoriaSelecionada == null)
            return Falha(nameof(dto.CategoriaId), "Selecione uma categoria válida.");

        if (ExisteProdutoComMesmoNomeNaCategoria(dto.Nome, dto.CategoriaId, dto.Id))
            return Falha(nameof(dto.Nome), "Já existe um produto com este nome nesta categoria.");

        Produto produtoAtualizado = new Produto(
            dto.Nome,
            categoriaSelecionada,
            dto.UnidadeMedida,
            dto.PrecoAproximado
        );

        Result resultadoValidacao = ValidarEntidade(produtoAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioProduto.Editar(dto.Id, produtoAtualizado);

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Produto? produto = repositorioProduto.SelecionarPorId(id);

        if (produto == null)
            return Result.Fail("Produto não encontrado.");

        repositorioProduto.Excluir(id);

        return Result.Ok();
    }

    public List<ListarProdutosDto> SelecionarTodos()
    {
        return repositorioProduto
            .SelecionarTodos()
            .Select(p => new ListarProdutosDto(
                p.Id,
                p.Nome,
                p.Categoria.Id,
                p.Categoria.Nome,
                p.Categoria.Cor,
                p.UnidadeMedida,
                p.PrecoAproximado
            ))
            .ToList();
    }

    public Result<DetalhesProdutoDto> SelecionarPorId(Guid id)
    {
        Produto? produto = repositorioProduto.SelecionarPorId(id);

        if (produto == null)
            return Result.Fail("Produto não encontrado.");

        return Result.Ok(new DetalhesProdutoDto(
            produto.Id,
            produto.Nome,
            produto.Categoria.Id,
            produto.Categoria.Nome,
            produto.Categoria.Cor,
            produto.UnidadeMedida,
            produto.PrecoAproximado
        ));
    }

    public List<OpcaoCategoriaDto> SelecionarCategorias()
    {
        return repositorioCategoria
            .SelecionarTodos()
            .Select(c => new OpcaoCategoriaDto(c.Id, c.Nome, c.Cor))
            .ToList();
    }

    private bool ExisteProdutoComMesmoNomeNaCategoria(string nome, Guid categoriaId, Guid? idIgnorado = null)
    {
        return repositorioProduto
            .SelecionarTodos()
            .Any(p =>
                p.Id != idIgnorado &&
                p.Categoria.Id == categoriaId &&
                string.Equals(p.Nome, nome, StringComparison.OrdinalIgnoreCase)
            );
    }

    private static Result ValidarEntidade(Produto produto)
    {
        List<string> erros = produto.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
}