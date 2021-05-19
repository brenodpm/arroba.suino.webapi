using System.Collections.Generic;
using System;
using arroba.suino.webapi.Domain.Entities;

namespace arroba.suino.webapi.Domain.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        void Insert(TEntity obj);
        void Update(TEntity obj);
        void Delete(Guid id);
        IList<TEntity> Select();
        TEntity Select(Guid id);
    }
}