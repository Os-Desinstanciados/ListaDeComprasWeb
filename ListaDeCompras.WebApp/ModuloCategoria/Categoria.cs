using ListaDeCompras.WebApp.Compartilhado;

namespace ListaDeCompras.WebApp.ModuloCategoria;

public class Categoria : EntidadeBase<Categoria>
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
        List<string> erros = new();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" é obrigatório.");

        if (Nome.Length > 50)
            erros.Add("O campo \"Nome\" deve conter no máximo 50 caracteres.");

        if (string.IsNullOrWhiteSpace(Cor))
            erros.Add("O campo \"Cor\" é obrigatório.");

        return erros;
    }

    public override void AtualizarDados(Categoria categoriaAtualizada)
    {
        Nome = categoriaAtualizada.Nome;
        Cor = categoriaAtualizada.Cor;
    }
}