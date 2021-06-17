using System;

namespace arroba.suino.webapi.Domain.DTO
{
    public class UsuarioDTO : BaseDTO
    {

        public virtual Guid Id { get; set; }
        public String Nome { get; set; }
    }
}