using ListaDeCompras.WebApp.Compartilhado.Dominio;

namespace ListaDeCompras.WebApp.ModuloCategoria.Dominio;

public sealed class Categoria : EntidadeBase<Categoria>
{
    public string Nome { get; set; } = string.Empty;    
    public string Cor { get; set; } = string.Empty;

    public Categoria() { }

    public Categoria(string nome, string cor)
    {
        Nome = nome;
        Cor = cor;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");

        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        if (string.IsNullOrWhiteSpace(Cor))
            erros.Add("O campo \"Cor\" deve ser preenchido.");        

        return erros;
    }

    public override void AtualizarDados(Categoria entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Cor = entidadeAtualizada.Cor;
    }
}
