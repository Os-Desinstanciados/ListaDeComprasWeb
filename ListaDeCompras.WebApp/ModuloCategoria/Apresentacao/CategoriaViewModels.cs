using System.ComponentModel.DataAnnotations;

namespace ListaDeCompras.WebApp.ModuloCategoria.Apresentacao;

public record ListarCategoriasViewModel(
    string Id,
    string Nome,
    string Cor
);

public record CadastrarCategoriaViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Cor\" deve ser preenchido.")]    
    string Cor
  
);

public record EditarCategoriaViewModel(
    string Id,

    [Required(ErrorMessage = "O campo \"Etiqueta\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Cor\" deve ser preenchido.")]    
    string Cor
    
);

public record ExcluirCategoriaViewModel(
    string Id,
    string Nome,
    string Cor
);
