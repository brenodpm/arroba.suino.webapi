

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using arroba.suino.webapi.Domain.Entities;
using arroba.suino.webapi.infra.Context;
using arroba.suino.webapi.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace arroba.suino.webapi.infra.Repository
{
    public class NewBaseType
    {
    }

    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(MySqlContext mySqlContext) : base(mySqlContext)
        {
        }
    }
}