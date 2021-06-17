using System.Collections.Generic;

namespace arroba.suino.webapi.Domain.DTO
{
    public abstract class BaseDTO
    {
        public IList<LinkDTO> links { get; set; } = new List<LinkDTO>();
       
    }
}