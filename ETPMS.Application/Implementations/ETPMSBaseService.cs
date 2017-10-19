using ETPMS.Application.Contracts;
using ETPMS.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ETPMS.Application.Implementations
{
    public abstract class ETPMSBaseService<TEntity> where TEntity : class, new()
    {
        internal IRepository<TEntity> Repository { get; private set; }
        protected ETPMSDbSession DbSession { get; private set; }

        public ETPMSBaseService(IRepository<TEntity> repository)
        {
            this.Repository = repository;
            this.DbSession = repository.DbSession;
        }
    }
}
