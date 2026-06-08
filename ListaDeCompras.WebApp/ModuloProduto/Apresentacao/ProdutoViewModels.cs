using System.ComponentModel.DataAnnotations;
using ListaDeCompras.WebApp.ModuloCategoria.Dominio;

namespace ListaDeCompras.WebApp.ModuloProduto.Apresentacao;

public record ListarProdutosViewModel(
    string Id,
    string Nome,
    string CategoriaNome,
    string Unidade,
    decimal Preco
);

public record CadastrarProdutoViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Categoria\" deve ser preenchido.")]    
    string CategoriaId,

    [Required(ErrorMessage = "O campo \"Unidade\" deve ser preenchido.")]    
    string Unidade,

    [Required(ErrorMessage = "O campo \"Preco\" deve ser preenchido.")]    
    decimal Preco
  
);

public record EditarProdutoViewModel(
    string Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Categoria\" deve ser preenchido.")]    
    string CategoriaId,

    [Required(ErrorMessage = "O campo \"Unidade\" deve ser preenchido.")]    
    string Unidade,

    [Required(ErrorMessage = "O campo \"Preco\" deve ser preenchido.")]    
    decimal Preco
    
);

public record ExcluirProdutoViewModel(
    string Id,
    string Nome,
    string CategoriaNome,
    string Unidade,
    decimal Preco
);
