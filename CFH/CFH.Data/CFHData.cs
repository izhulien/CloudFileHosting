﻿namespace CFH.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using CFH.Data.Contracts;
    using CFH.Data.Repositories;
    using CFH.Models;

    public class CFHData : ICFHData
    {
        private readonly DbContext context;
        private readonly IDictionary<Type, object> repositories = new Dictionary<Type, object>();

        public CFHData(DbContext context)
        {
            this.context = context;
        }

        public CFHData()
            : this(new ApplicationDbContext())
        {
        }

        public IGenericRepository<File> Files
        {
            get
            {
                return this.GetRepository<File>();
            }
        }

        public IGenericRepository<ApplicationUser> Users
        {
            get
            {
                return this.GetRepository<ApplicationUser>();
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
