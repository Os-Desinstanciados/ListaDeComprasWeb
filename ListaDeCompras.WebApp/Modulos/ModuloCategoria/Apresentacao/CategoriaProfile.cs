using AutoMapper;
using ListaDeCompras.WebApp.Modulos.ModuloCategoria.Aplicacao;

namespace ListaDeCompras.WebApp.Modulos.ModuloCategoria.Apresentacao;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        CreateMap<ListarCategoriasDto, ListarCategoriasViewModel>();
        CreateMap<CadastrarCategoriaViewModel, CadastrarCategoriaDto>();
        CreateMap<EditarCategoriaViewModel, EditarCategoriaDto>();
        CreateMap<DetalhesCategoriaDto, EditarCategoriaViewModel>();
        CreateMap<DetalhesCategoriaDto, ExcluirCategoriaViewModel>();
    }
}