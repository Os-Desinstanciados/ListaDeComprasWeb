// DTO = Data Transfer Object

namespace ListaDeCompras.WebApp.ModuloLista.Aplicacao;

public record ListarListasDto(
    string Id,
    string Nome,
    DateTime DataCriacao
);

public record CadastrarListaDto(
    string Nome,
    DateTime DataCriacao
);

public record EditarListaDto(
    string Id,
    string Nome,
    DateTime DataCriacao
);

public record DetalhesListaDto(
    string Id,
    string Nome,
    DateTime DataCriacao,
    List<ExibirItemListaDto> Itens, 
    decimal TotalGasto              
);

public record ExibirItemListaDto(
    string Id,
    string ProdutoId,
    string ProdutoNome,
    decimal Preco,
    int Quantidade,
    decimal PrecoTotal
);

public record AdicionarItemDto(
    string ProdutoId,
    int Quantidade
);