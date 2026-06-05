using System.ComponentModel.DataAnnotations;

namespace ListaDeCompras.WebApp.Models;

public record ListarProdutoViewModel(
    string Id,
    string Nome,
    string Categoria,
    string UnidadeMedida,
    decimal PrecoAproximado
);

public record CadastrarProdutoViewModel(

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Categoria\" deve ser preenchido.")]
    string CategoriaId,

    [Required(ErrorMessage = "O campo \"Unidade de Medida\" deve ser preenchido.")]
    string UnidadeMedida,

    [Range(0.01, double.MaxValue, ErrorMessage = "O campo \"Preço Aproximado\" deve ser maior que zero.")]
    decimal PrecoAproximado
);

public record EditarProdutoViewModel(

    string Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Categoria\" deve ser preenchido.")]
    string CategoriaId,

    [Required(ErrorMessage = "O campo \"Unidade de Medida\" deve ser preenchido.")]
    string UnidadeMedida,

    [Range(0.01, double.MaxValue, ErrorMessage = "O campo \"Preço Aproximado\" deve ser maior que zero.")]
    decimal PrecoAproximado
);

public record ExcluirProdutoViewModel(
    string Id,
    string Nome,
    string Categoria,
    string UnidadeMedida,
    decimal PrecoAproximado
);
