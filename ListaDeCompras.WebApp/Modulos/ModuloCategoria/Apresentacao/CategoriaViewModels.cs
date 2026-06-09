using System.ComponentModel.DataAnnotations;

namespace ListaDeCompras.WebApp.Modulos.ModuloCategoria.Apresentacao;

public record ListarCategoriasViewModel(
    Guid Id,
    string Nome,
    string Cor
);

public record CadastrarCategoriaViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(50, ErrorMessage = "O campo \"Nome\" deve conter no máximo 50 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Cor\" deve ser preenchido.")]
    string Cor
);

public record EditarCategoriaViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(50, ErrorMessage = "O campo \"Nome\" deve conter no máximo 50 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Cor\" deve ser preenchido.")]
    string Cor
);

public record ExcluirCategoriaViewModel(
    Guid Id,
    string Nome,
    string Cor
);