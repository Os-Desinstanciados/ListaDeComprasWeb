using AutoMapper;
using ListaDeCompras.WebApp.Modulos.ModuloProduto.Aplicacao;
using ListaDeCompras.WebApp.Modulos.ModuloProduto.Apresentacao;

namespace ListaDeCompras.WebApp.ModuloProduto.Apresentacao;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<OpcaoCategoriaDto, OpcaoCategoriaViewModel>();
        CreateMap<ListarProdutosDto, ListarProdutosViewModel>();
        CreateMap<CadastrarProdutoViewModel, CadastrarProdutoDto>();
        CreateMap<EditarProdutoViewModel, EditarProdutoDto>();

        CreateMap<DetalhesProdutoDto, EditarProdutoViewModel>()
            .ForCtorParam("Categorias", opt => opt.MapFrom(_ => new List<OpcaoCategoriaViewModel>()));

        CreateMap<DetalhesProdutoDto, ExcluirProdutoViewModel>();
    }
}