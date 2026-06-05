// DTO = Data Transfer Object

namespace ListaDeCompras.WebApp.ModuloCategoria.Aplicacao;

public record ListarCategoriasDto(
    string Id,
    string Nome,
    string Cor
);

public record CadastrarCategoriaDto(
    string Nome,
    string Cor
);

public record EditarCategoriaDto(
    string Id,
    string Nome,
    string Cor
);

public record DetalhesCategoriaDto(
    string Id,
    string Nome,
    string Cor
);
