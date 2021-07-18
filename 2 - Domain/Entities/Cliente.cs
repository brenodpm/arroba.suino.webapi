using System;

namespace arroba.suino.webapi.Domain.Entities
{
    public class Cliente
    {
        public Guid ApiKey { get; set; }
        public string Nome { get; set; }
        public Guid ApiSecret { get; set; }
        public bool Ativo { get; set; }
        public Guid CodDesenvolvedor { get; set; }
    }
}