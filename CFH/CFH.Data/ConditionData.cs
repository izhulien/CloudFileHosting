namespace CFH.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using CFH.Data.Contracts;
    using CFH.Data.Repositories;
    using CFH.Models;

    public class ConditionData : IConditioData
    {
        private readonly DbContext context;
        private readonly IDictionary<Type, object> repositories = new Dictionary<Type, object>();

        public ConditionData(DbContext context)
        {
            this.context = context;
        }

        public ConditionData()
            : this(new ApplicationDbContext())
        {
        }

        public IGenericRepository<Condition> Conditions
        {
            get
            {
                return this.GetRepository<Condition>();
            }
        }

        public IGenericRepository<Directory> Directories
        {
            get
            {
                return this.GetRepository<Directory>();
            }
        }


        public IGenericRepository<File> Files
        {
            get
            {
                return this.GetRepository<File>();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);

            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var typeOfRepository = typeof(GenericRepository<T>);

                this.repositories.Add(typeOfModel, Activator.CreateInstance(typeOfRepository, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeOfModel];
        }
    }
}
