using ListaDeCompras.WebApp.Compartilhado.Dominio;
using ListaDeCompras.WebApp.ModuloCategoria.Dominio;

namespace ListaDeCompras.WebApp.ModuloProduto.Dominio;

public sealed class Produto : EntidadeBase<Produto>
{
    public string Nome { get; set; } = string.Empty;    
    public Categoria Categoria { get; set; } = null!;
    public string Unidade { get; set; } = string.Empty;
    public decimal Preco { get; set; } = 0;

    public Produto() { }

    public Produto(string nome, Categoria categoria, string unidade, decimal preco)
    {
        Nome = nome;
        Categoria = categoria;
        Unidade = unidade;
        Preco = preco;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");

        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        if (Categoria == null)
            erros.Add("O campo \"Categoria\" deve ser preenchido.");

        if (string.IsNullOrWhiteSpace(Unidade))
            erros.Add("O campo \"Unidade\" deve ser preenchido.");

        if (Preco == 0)
            erros.Add("O campo \"Preço\" deve ter um valor maior que 0.");        

        return erros;
    }

    public override void AtualizarDados(Produto entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Categoria = entidadeAtualizada.Categoria;
        Unidade = entidadeAtualizada.Unidade;
        Preco = entidadeAtualizada.Preco;
    }
}
