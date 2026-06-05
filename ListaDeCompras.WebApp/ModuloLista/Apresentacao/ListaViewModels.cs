using System.ComponentModel.DataAnnotations;

namespace ListaDeCompras.WebApp.ModuloLista.Apresentacao;

public record ListarListasViewModel(
    string Id,
    string Nome,
    DateTime DataCriacao
);

public record CadastrarListaViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    DateTime DataCriacao
  
);

public record EditarListaViewModel(
    string Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    DateTime DataCriacao
    
);

public record ExcluirListaViewModel(
    string Id,
    string Nome,
    DateTime DataCriacao
);
