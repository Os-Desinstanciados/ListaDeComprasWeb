namespace ListaDeCompras.WebApp.Models;

public record ListarProdutoViewModel(
    string Id,
    string Nome,
    string Categoria,
    string UnidadeMedida,
    decimal PrecoAproximado
);

public record CadastrarProdutoViewModel(
    string Nome,
    string CategoriaId,
    string UnidadeMedida,
    decimal PrecoAproximado
);

public record EditarProdutoViewModel(
    string Id,
    string Nome,
    string CategoriaId,
    string UnidadeMedida,
    decimal PrecoAproximado
);

public record ExcluirProdutoViewModel(
    string Id,
    string Nome,
    string Categoria,
    string UnidadeMedida,
    decimal PrecoAproximado
);