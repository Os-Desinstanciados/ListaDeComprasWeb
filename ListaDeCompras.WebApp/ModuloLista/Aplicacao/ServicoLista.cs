using ListaDeCompras.WebApp.ModuloLista.Dominio;
using ListaDeCompras.WebApp.ModuloProduto.Dominio;
using FluentResults;

namespace ListaDeCompras.WebApp.ModuloLista.Aplicacao;

public class ServicoLista
{
    private readonly IRepositorioLista repositorioLista;
    private readonly IRepositorioProduto repositorioProduto;    

    public ServicoLista(
        IRepositorioLista repositorioLista,
        IRepositorioProduto repositorioProduto
        
    )
    {
        this.repositorioLista = repositorioLista; 
        this.repositorioProduto = repositorioProduto;      
    }

    public Result Cadastrar(CadastrarListaDto dto)
    {
        if (ExisteListaComNome(dto.Nome))
            return Falha("Nome", "Já existe uma lista com este nome.");

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
            return Falha("Nome", "Já existe uma lista com este nome.");

        Lista listaAtualizada = new Lista(dto.Nome, dto.DataCriacao);

        bool conseguiuEditar = repositorioLista.Editar(dto.Id, listaAtualizada);

        if (!conseguiuEditar)
            return Result.Fail("Lista não encontrada.");

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
            return Result.Fail("Lista não encontrada.");
       
        var itensDto = lista.Itens.Select(i => new ExibirItemListaDto(
            i.Id,
            i.Produto.Id,       
            i.Produto.Nome,
            i.Produto.Preco,            
            i.Quantidade,
            i.PrecoTotal 
        )).ToList();

       
        var detalhesDto = new DetalhesListaDto(
            lista.Id, 
            lista.Nome, 
            lista.DataCriacao, 
            itensDto, 
            lista.TotalGasto
        );

        return Result.Ok(detalhesDto);
    }

    
    public Result AdicionarItem(string listaId, AdicionarItemDto dto)
    {
        Lista? lista = repositorioLista.SelecionarPorId(listaId);
        if (lista == null)
            return Result.Fail("Lista de compras não encontrada.");
       
        Produto? produto = repositorioProduto.SelecionarPorId(dto.ProdutoId);
        if (produto == null)
            return Falha("ProdutoId", "O produto selecionado não existe.");

        if (dto.Quantidade <= 0)
            return Falha("Quantidade", "A quantidade deve ser maior que zero.");
        
        lista.AdicionarItem(produto, dto.Quantidade);
       
        repositorioLista.Editar(listaId, lista);

        return Result.Ok();
    }

    public Result RemoverItem(string listaId, string itemId)
    {
        Lista? lista = repositorioLista.SelecionarPorId(listaId);
        if (lista == null)
            return Result.Fail("Lista de compras não encontrada.");
       
        bool itemRemovido = lista.RemoverItem(itemId);
        if (!itemRemovido)
            return Result.Fail("Item não encontrado nesta lista.");
       
        repositorioLista.Editar(listaId, lista);

        return Result.Ok();
    }
  

    private bool ExisteListaComNome(string nome, string? idIgnorado = null)
    {
        List<Lista> listas = repositorioLista.SelecionarTodos();

        foreach (Lista l in listas)
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
