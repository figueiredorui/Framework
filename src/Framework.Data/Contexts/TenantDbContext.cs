using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace Framework.Data
{
    public abstract class TenantDbContext : DbContext, IDbModelCacheKeyProvider
    {
        public TenantDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public override int SaveChanges()
        {
            try
            {
                var retVal = base.SaveChanges();
                return retVal;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = new List<string>();
                foreach (DbEntityValidationResult validationResult in ex.EntityValidationErrors)
                {
                    string entityName = validationResult.Entry.Entity.GetType().Name;
                    foreach (DbValidationError error in validationResult.ValidationErrors)
                    {
                        errorMessages.Add(entityName + "." + error.PropertyName + ": " + error.ErrorMessage);
                    }
                }

                throw new Exception(String.Join("\n", errorMessages.ToArray()));
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected abstract string SchemaName { get; set; }

        protected abstract void ModelConfigurations(DbModelBuilder modelBuilder);

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (this.SchemaName != null)
            {
                modelBuilder.HasDefaultSchema(this.SchemaName);
            }

            ModelConfigurations(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public string CacheKey
        {
            get { return this.SchemaName; }
        }
    }
}
