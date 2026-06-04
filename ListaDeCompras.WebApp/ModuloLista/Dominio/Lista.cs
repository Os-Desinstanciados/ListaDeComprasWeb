using ListaDeCompras.WebApp.Compartilhado.Dominio;

namespace ListaDeCompras.WebApp.ModuloLista.Dominio;

public sealed class Lista : EntidadeBase<Lista>
{
    public string Nome { get; set; } = string.Empty;    
    public DateTime DataCriacao { get; set; } = DateTime.Now;

    public Lista() { }

    public Lista(string nome, DateTime dataCriacao)
    {
        Nome = nome;
        DataCriacao = dataCriacao;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");

        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");        

        return erros;
    }

    public override void AtualizarDados(Lista entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        DataCriacao = entidadeAtualizada.DataCriacao;
    }
}
