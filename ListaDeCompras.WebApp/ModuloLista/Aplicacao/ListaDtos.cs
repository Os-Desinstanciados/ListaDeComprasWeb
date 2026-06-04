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
    DateTime DataCriacao
);
