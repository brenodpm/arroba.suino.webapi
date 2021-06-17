using System.Collections.Generic;
using System;
using arroba.suino.webapi.Domain.Entities;
using System.Threading.Tasks;

namespace arroba.suino.webapi.Interfaces.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task Insert(TEntity obj);
        Task Update(TEntity obj);
        Task Delete(Guid id);
        Task<IList<TEntity>> Select();
        Task<TEntity> Select(Guid id);
    }
}