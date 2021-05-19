using System.Collections.Generic;
using System;
using arroba.suino.webapi.Domain.Entities;
using FluentValidation;

namespace arroba.suino.webapi.Domain.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        TEntity Add<TValidator> (TEntity obj) where TValidator: AbstractValidator<TEntity>;
        void Delete(Guid id);
        IList<TEntity> Get();
        TEntity GetById(Guid id);
        TEntity Update<TValidator> (TEntity obj) where TValidator: AbstractValidator<TEntity>;
    }
}