namespace CFH.Data.Contracts
{
    using System;
    using CFH.Models;

    public interface ICFHData : IDisposable
    {
        IGenericRepository<File> Files { get; }

        IGenericRepository<ApplicationUser> Users{ get; }

        int SaveChanges();
    }
}
