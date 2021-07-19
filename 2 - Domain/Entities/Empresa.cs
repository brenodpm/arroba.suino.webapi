using System;

namespace arroba.suino.webapi.Domain.Entities
{
    public class Empresa: BaseEntity
    {
        public string NomeEmpresa { get; set; }
        public Guid Security { get; set; }
        public string Email { get; set; }
        public bool EmailValidado { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}