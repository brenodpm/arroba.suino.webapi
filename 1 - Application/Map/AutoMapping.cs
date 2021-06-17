using arroba.suino.webapi.Domain.DTO;
using arroba.suino.webapi.Domain.Entities;
using AutoMapper;

namespace arroba.suino.webapi.Application.Map
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Usuario, UsuarioDTO>();
        }
    }
}