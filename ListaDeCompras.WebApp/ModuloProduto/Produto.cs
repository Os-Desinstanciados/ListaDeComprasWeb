using ListaDeCompras.WebApp.Compartilhado;
using ListaDeCompras.WebApp.ModuloCategoria;

namespace ListaDeCompras.WebApp.ModuloProduto;

public class Produto : EntidadeBase<Produto>
{
    public string Nome { get; set; } = string.Empty;
    public Categoria Categoria { get; set; } = null!;
    public string UnidadeMedida { get; set; } = string.Empty;
    public decimal PrecoAproximado { get; set; }

    public Produto()
    {
        
    }

    public Produto(string nome, Categoria categoria, string unidadeMedida, decimal precoAproximado)
    {
        Nome = nome;
        Categoria = categoria;
        UnidadeMedida = unidadeMedida;
        PrecoAproximado = precoAproximado;
    }

    public override List<string> Validar()
    {
        List<string> erros = new();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" é obrigatório.");

        else if (Nome.Length < 2 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 2 e 100 caracteres.");

        if (Categoria == null)
            erros.Add("O campo \"Categoria\" é obrigatório.");

        if (string.IsNullOrWhiteSpace(UnidadeMedida))
            erros.Add("O campo \"Unidade de medida\" é obrigatório.");

        if (PrecoAproximado <= 0)
            erros.Add("O campo \"Preço aproximado\" deve ser maior que zero.");

        return erros;
    }

    public override void AtualizarDados(Produto produtoAtualizado)
    {
        Nome = produtoAtualizado.Nome;
        Categoria = produtoAtualizado.Categoria;
        UnidadeMedida = produtoAtualizado.UnidadeMedida;
        PrecoAproximado = produtoAtualizado.PrecoAproximado;
    }
}