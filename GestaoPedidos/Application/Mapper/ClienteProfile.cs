using AutoMapper;
using GestaoPedidos.Application.DTO;
using GestaoPedidos.Domain.Entities;

namespace GestaoPedidos.Application.Mapper
{
    public class ClienteProfile : Profile
    {
       public ClienteProfile()
        {
            CreateMap<Cliente, ClienteResponseDTO>(); 
            CreateMap<Cliente, ClienteCreateDTO>();
            CreateMap<Cliente, ClienteUpdateDTO>();
        }
    }
}
