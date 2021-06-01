using System.Collections.Generic;
using System.Linq;
using arroba.suino.webapi.Domain.Interfaces;
using arroba.suino.webapi.infra.Context;

namespace arroba.suino.webapi.Usuario.UseCases
{
    public class UseCase : IUseCase
    {
        private readonly MySqlContext db;
        public UseCase(MySqlContext db)
        {
            this.db = db;
        }
        public IList<Domain.Entities.Usuario> GetUsuarios()
        {
            var usuario = db.Usuarios.ToList();
            return usuario;

        }
    }
}