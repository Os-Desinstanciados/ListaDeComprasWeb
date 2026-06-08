namespace ListaDeCompras.WebApp.Modulos.ModuloCategoria.Aplicacao;

public record ListarCategoriasDto(
    Guid Id,
    string Nome,
    string Cor
);

public record CadastrarCategoriaDto(
    string Nome,
    string Cor
);

public record EditarCategoriaDto(
    Guid Id,
    string Nome,
    string Cor
);

public record DetalhesCategoriaDto(
    Guid Id,
    string Nome,
    string Cor
);