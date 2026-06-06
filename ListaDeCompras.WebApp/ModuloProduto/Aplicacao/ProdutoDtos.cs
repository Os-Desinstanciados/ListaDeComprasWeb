// DTO = Data Transfer Object

using ListaDeCompras.WebApp.ModuloCategoria.Dominio;

namespace ListaDeCompras.WebApp.ModuloProduto.Aplicacao;

public record ListarProdutosDto(
    string Id,
    string Nome,
    Categoria Categoria,
    string Unidade,
    decimal Preco
);

public record CadastrarProdutoDto(
    string Nome,
    Categoria Categoria,
    string Unidade,
    decimal Preco
);

public record EditarProdutoDto(
    string Id,
    string Nome,
    Categoria Categoria,
    string Unidade,
    decimal Preco
);

public record DetalhesProdutoDto(
    string Id,
    string Nome,
    Categoria Categoria,
    string Unidade,
    decimal Preco
);
