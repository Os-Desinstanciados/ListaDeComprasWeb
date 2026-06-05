namespace ListaDeCompras.WebApp.Models;
public record ListarCategoriasViewModel(
    string Id,
    string Nome,
    string Cor
);

public record CadastrarCategoriaViewModel(
    string Id,
    string Nome,
    string Cor
);

public record EditarCategoriaViewModel(
    string Id,
    string Nome,
    string Cor
);

public record ExcluirCategoriaViewModel(
    string Id,
    string Nome,
    string Cor
);