using System;

namespace arroba.suino.webapi.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public virtual Guid Grupo { get; set; }
        public String Nome { get; set; }
    
    }
}