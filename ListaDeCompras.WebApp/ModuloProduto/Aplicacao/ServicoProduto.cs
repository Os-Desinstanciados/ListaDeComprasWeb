using ListaDeCompras.WebApp.ModuloProduto.Dominio;
using ListaDeCompras.WebApp.ModuloCategoria.Dominio;
using FluentResults;

namespace ListaDeCompras.WebApp.ModuloProduto.Aplicacao;

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
        Categoria? categoria = repositorioCategoria.SelecionarPorId(dto.CategoriaId);

        if (categoria == null)
            return Falha("CategoriaId", "A categoria informada não existe.");

        if (ExisteProdutoComNome(dto.Nome))
            return Falha("Nome", "Já existe um produto com este nome.");

        Produto novoProduto = new Produto(
            dto.Nome,
            categoria,
            dto.Unidade,
            dto.Preco
        );

        repositorioProduto.Cadastrar(novoProduto);

        return Result.Ok();
    }

    public Result Editar(EditarProdutoDto dto)
    {
        if (ExisteProdutoComNome(dto.Nome, dto.Id))
            return Falha("Nome", "Já existe um produto com este nome.");

        Categoria? categoria = repositorioCategoria.SelecionarPorId(dto.CategoriaId);

        if (categoria == null)
            return Falha("CategoriaId", "A categoria informada não existe.");

        Produto ProdutoAtualizado = new Produto(dto.Nome, categoria, dto.Unidade, dto.Preco);

        bool conseguiuEditar = repositorioProduto.Editar(dto.Id, ProdutoAtualizado);

        if (!conseguiuEditar)
            return Result.Fail("Produto não encontrado.");

        return Result.Ok();
    }

    public Result Excluir(string id)
    {
        Produto? Produto = repositorioProduto.SelecionarPorId(id);

        if (Produto == null)
            return Result.Fail("Produto não encontrado.");
        
        repositorioProduto.Excluir(id);

        return Result.Ok();
    }

    public List<ListarProdutosDto> SelecionarTodos()
    {
        List<Produto> produtos = repositorioProduto.SelecionarTodos();

        return produtos
            .Select(p => new ListarProdutosDto(p.Id, p.Nome, p.Categoria.Nome, p.Unidade, p.Preco))
            .ToList();
    }

    public Result<DetalhesProdutoDto> SelecionarPorId(string id)
    {
        Produto? produto = repositorioProduto.SelecionarPorId(id);

        if (produto == null)
            return Result.Fail("Produto não encontrado.");

        return Result.Ok(new DetalhesProdutoDto(produto.Id, produto.Nome, produto.Categoria.Nome, produto.Unidade, produto.Preco));
    }

    private bool ExisteProdutoComNome(string nome, string? idIgnorado = null)
    {
        List<Produto> produtos = repositorioProduto.SelecionarTodos();

        foreach (Produto p in produtos)
        {
            if (p.Id != idIgnorado && string.Equals(p.Nome, nome, StringComparison.OrdinalIgnoreCase))
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
