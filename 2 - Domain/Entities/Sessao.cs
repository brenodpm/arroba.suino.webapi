using System;

namespace arroba.suino.webapi.Domain.Entities
{
    public class Sessao: BaseEntity
    {
        public Guid CodEmpresa { get; set; }
        public Guid CodUsuario { get; set; }
        public Guid CodGrupo { get; set; }
        public Guid ApiKey { get; set; }
        public DateTime PrimeiroAcesso { get; set; }
        public DateTime UltimoAcesso { get; set; }
        public string Descricao { get; set; }
        public string Localizacao { get; set; }
    }
}