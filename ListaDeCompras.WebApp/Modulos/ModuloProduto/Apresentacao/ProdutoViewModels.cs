using System.ComponentModel.DataAnnotations;
using ListaDeCompras.WebApp.Modulos.ModuloProduto.Dominio;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ListaDeCompras.WebApp.Modulos.ModuloProduto.Apresentacao;

public record OpcaoCategoriaViewModel(
    Guid Id,
    string Nome,
    string Cor
);

public record ListarProdutosViewModel(
    Guid Id,
    string Nome,
    Guid CategoriaId,
    string CategoriaNome,
    string CategoriaCor,
    UnidadeMedida UnidadeMedida,
    decimal PrecoAproximado
);

public record CadastrarProdutoViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Categoria\" deve ser preenchido.")]
    Guid CategoriaId,

    UnidadeMedida UnidadeMedida,

    [Range(0, double.MaxValue, ErrorMessage = "O campo \"Preço Aproximado\" deve conter um valor maior ou igual a 0.")]
    decimal PrecoAproximado,

    [ValidateNever]
    List<OpcaoCategoriaViewModel> Categorias
);

public record EditarProdutoViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Categoria\" deve ser preenchido.")]
    Guid CategoriaId,

    UnidadeMedida UnidadeMedida,

    [Range(0, double.MaxValue, ErrorMessage = "O campo \"Preço Aproximado\" deve conter um valor maior ou igual a 0.")]
    decimal PrecoAproximado,

    [ValidateNever]
    List<OpcaoCategoriaViewModel> Categorias
);

public record ExcluirProdutoViewModel(
    Guid Id,
    string Nome,
    Guid CategoriaId,
    string CategoriaNome,
    string CategoriaCor,
    UnidadeMedida UnidadeMedida,
    decimal PrecoAproximado
);