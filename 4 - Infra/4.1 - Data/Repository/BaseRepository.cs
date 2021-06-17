using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using arroba.suino.webapi.Domain.Entities;
using arroba.suino.webapi.infra.Context;
using arroba.suino.webapi.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace arroba.suino.webapi.infra.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly MySqlContext db;

        public BaseRepository(MySqlContext mySqlContext)
        {
            db = mySqlContext;
        }

        public async Task Insert(TEntity obj)
        {
            await db.Set<TEntity>().AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task Update(TEntity obj)
        {
            db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            TEntity entity = await Select(id);
            entity.Ativo = false;
            await Update(entity);
         }

        public async Task<IList<TEntity>> Select() =>
            await db.Set<TEntity>().Where(t => t.Ativo).ToListAsync();
            
        public async Task<TEntity> Select(Guid id) =>
            await db.Set<TEntity>().FindAsync(id);
    }
}