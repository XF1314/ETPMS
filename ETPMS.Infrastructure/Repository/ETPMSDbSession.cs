using ETPMS.Entity;
using ETPMS.Infrastructure.Components;
using ETPMS.Infrastructure.Extensions;
using ETPMS.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ETPMS.Infrastructure.Repository
{
    [Component(LifeStyle.InstancePerLifetimeScope)]
    public sealed class ETPMSDbSession : IUnitOfWork
    {
        public ETPMSDbContext DbContext { get; private set; }
        private ILogger _logger;

        public ETPMSDbSession()
        {
            this.DbContext = new ETPMSDbContext();
            this._logger = ObjectContainer.Resolve<ILoggerFactory>().Create(GetType().FullName);
        }

        public void Commite()
        {
            try
            {
                var validationErrors = this.DbContext.GetValidationErrors().Where(k => !k.IsValid);
                if (!validationErrors.Any())
                    this.DbContext.SaveChanges();
                else
                {
                    var errorMsg = string.Empty;
                    validationErrors.ForEach(k =>
                    {
                        k.ValidationErrors.ForEach(s => { errorMsg += $"{s.PropertyName}：{s.ErrorMessage}<br/>"; });
                    });
                    throw (new ArgumentException(errorMsg));
                }
            }
            catch (Exception ex)
            {
                this._logger.Fatal(ex);
                throw ex;
            }
        }

        public void RollBackChanges()
        {
            this.DbContext.ChangeTracker.Entries()
                .ToList().ForEach(o => o.State = EntityState.Unchanged);
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : class
        {
            this.DbContext.Entry<TEntity>(entity).State = EntityState.Unchanged;
        }

        public void Modify<TEntity>(TEntity entity) where TEntity : class
        {
            this.DbContext.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public void Modify<TEntity>(TEntity originalEntity, TEntity newEntity) where TEntity : class
        {
            if (this.DbContext.Entry<TEntity>(originalEntity).State != EntityState.Unchanged)
                this.DbContext.Entry<TEntity>(originalEntity).State = EntityState.Unchanged;
            this.DbContext.Entry<TEntity>(originalEntity).CurrentValues.SetValues(newEntity);
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return this.DbContext.Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return this.DbContext.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        public IDbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return this.DbContext.Set<TEntity>();
        }

        public void Dispose()
        {
            try
            {
                this.Commite();
            }
            finally
            {
                this.DbContext.Dispose();
            }
        }
    }
}
