using AutoMapper;
using ConcessionariaApp.Application.Dtos;
using ConcessionariaApp.Models;
using ConcessionariApp.Models;

namespace ConcessionariaApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {            
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<Concessionaria, ConcessionariaDto>().ReverseMap();
            CreateMap<Fabricante, FabricanteDto>().ReverseMap();
            CreateMap<Relatorio, RelatorioDto>().ReverseMap();            
            CreateMap<Veiculo, VeiculoDto>().ReverseMap();
            CreateMap<Usuario, VendaDto>().ReverseMap();
            CreateMap<Venda, VendaDto>().ReverseMap();
        }
    }
}
