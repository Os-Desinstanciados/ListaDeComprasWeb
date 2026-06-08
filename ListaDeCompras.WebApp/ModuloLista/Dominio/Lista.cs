using ListaDeCompras.WebApp.Compartilhado.Dominio;
using ListaDeCompras.WebApp.ModuloProduto.Dominio;

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

    public List<ItemListaCompras> Itens { get; set; } = new List<ItemListaCompras>();
    public decimal TotalGasto
    {
        get
        {
            decimal totalGasto = 0;

            foreach (ItemListaCompras item in Itens)
                totalGasto += item.PrecoTotal;

            return totalGasto;
        }
    }

    public void AdicionarItem(Produto produto, int quantidade)
    {
        ItemListaCompras item = new ItemListaCompras(produto, quantidade);

        Itens.Add(item);
    }

    public bool RemoverItem(string idItem)
    {
        foreach (ItemListaCompras item in Itens)
        {
            if (item.Id == idItem)
            {
                Itens.Remove(item);
                return true;
            }
        }

        return false;
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
