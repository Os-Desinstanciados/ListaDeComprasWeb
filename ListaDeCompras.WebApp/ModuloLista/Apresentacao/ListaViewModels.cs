using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

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

public record DetalhesListaViewModel(
    string Id,
    string Nome,
    DateTime DataCriacao,
    List<ItemListaViewModel> Itens,
    decimal TotalGasto
);

public record ItemListaViewModel(
    string Id, 
    string ProdutoNome,
    decimal Preco,
    int Quantidade,
    decimal PrecoTotal
);

public class AdicionarItemViewModel
{
    public string ListaId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Você precisa selecionar um produto.")]
    public string ProdutoId { get; set; } = string.Empty;

    [Required(ErrorMessage = "A quantidade deve ser informada.")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser de pelo menos 1 item.")]
    public int Quantidade { get; set; } = 1;

    
    public List<SelectListItem> ProdutosDisponiveis { get; set; } = new();
}
