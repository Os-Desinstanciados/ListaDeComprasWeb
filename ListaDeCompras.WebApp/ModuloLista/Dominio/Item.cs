using System.Security.Cryptography;
using ListaDeCompras.WebApp.ModuloProduto.Dominio;

namespace ListaDeCompras.WebApp.ModuloLista.Dominio;

public class ItemListaCompras
{
    public string Id { get; set; }
    public Produto Produto { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoTotal 
    {
        get
        {
            return Produto.Preco * Quantidade;
        }
    }

    public ItemListaCompras()
    {
    }

    public ItemListaCompras(Produto produto, int quantidade)
    {
        Id = Convert
                .ToHexString(RandomNumberGenerator.GetBytes(4))
                .ToLower()
                .Substring(0, 7);

        Produto = produto;
        Quantidade = quantidade;
    }
}
