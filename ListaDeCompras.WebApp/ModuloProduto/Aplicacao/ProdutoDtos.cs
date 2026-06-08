// DTO = Data Transfer Object

using ListaDeCompras.WebApp.ModuloCategoria.Dominio;

namespace ListaDeCompras.WebApp.ModuloProduto.Aplicacao;

public record ListarProdutosDto(
    string Id,
    string Nome,
    string CategoriaNome,
    string Unidade,
    decimal Preco
);

public record CadastrarProdutoDto(
    string Nome,
    string CategoriaId,
    string Unidade,
    decimal Preco
);

public record EditarProdutoDto(
    string Id,
    string Nome,
    string CategoriaId,
    string Unidade,
    decimal Preco
);

public record DetalhesProdutoDto(
    string Id,
    string Nome,
    string CategoriaNome,
    string Unidade,
    decimal Preco
);
