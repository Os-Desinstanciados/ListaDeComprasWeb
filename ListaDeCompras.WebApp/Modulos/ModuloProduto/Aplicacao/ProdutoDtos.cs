using ListaDeComprasWeb.WebApp.Modulos.ModuloProduto.Dominio;

namespace ListaDeComprasWeb.WebApp.Modulos.ModuloProduto.Aplicacao;

public record OpcaoCategoriaDto(
    Guid Id,
    string Nome,
    string Cor
);

public record ListarProdutosDto(
    Guid Id,
    string Nome,
    Guid CategoriaId,
    string CategoriaNome,
    string CategoriaCor,
    UnidadeMedida UnidadeMedida,
    decimal PrecoAproximado
);

public record CadastrarProdutoDto(
    string Nome,
    Guid CategoriaId,
    UnidadeMedida UnidadeMedida,
    decimal PrecoAproximado
);

public record EditarProdutoDto(
    Guid Id,
    string Nome,
    Guid CategoriaId,
    UnidadeMedida UnidadeMedida,
    decimal PrecoAproximado
);

public record DetalhesProdutoDto(
    Guid Id,
    string Nome,
    Guid CategoriaId,
    string CategoriaNome,
    string CategoriaCor,
    UnidadeMedida UnidadeMedida,
    decimal PrecoAproximado
);