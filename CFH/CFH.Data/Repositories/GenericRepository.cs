﻿namespace CFH.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using CFH.Data.Contracts;

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public GenericRepository()
            : this(new ApplicationDbContext())
        {
        }

        public GenericRepository(ICFHDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.", nameof(context));
            }

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        protected IDbSet<T> DbSet { get; set; }

        protected ICFHDbContext Context { get; set; }

        public virtual IQueryable<T> All()
        {
            return this.DbSet.AsQueryable();
        }

        public virtual IQueryable<T> Search(Expression<Func<T, bool>> condition)
        {
            return this.All().Where(condition);
        }

        public virtual T Find(int id)
        {
            return this.DbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Added);
        }

        public virtual void Update(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Modified);
        }

        public virtual void Delete(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Deleted);
        }

        public virtual void Delete(int id)
        {
            var entity = this.Find(id);

            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        public virtual void Detach(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Detached);
        }

        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }

        private void ChangeEntityState(T entity, EntityState newEntityState)
        {
            var entry = this.Context.Entry(entity);
            entry.State = newEntityState;
        }

        //public void Upload(T entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Download(T entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
