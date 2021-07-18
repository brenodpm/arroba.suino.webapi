using System;

namespace arroba.suino.webapi.Domain.Entities
{
    public class Cliente
    {
        public string ApiKey { get; set; }
        public string Nome { get; set; }
        public string ApiSecret { get; set; }
        public bool Ativo { get; set; }
        public string CodDesenvolvedor { get; set; }
    }
}