using System;
using System.Collections.Generic;

namespace arroba.suino.webapi.Domain.Entities
{
    public class Grupo: BaseEntity
    {
        public Guid CodEmpresa { get; set; }
        public string NomeGrupo { get; set; }
        public IList<string> Permissoes { get; set; }
    }
}