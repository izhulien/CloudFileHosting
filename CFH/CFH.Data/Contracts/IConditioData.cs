namespace CFH.Data.Contracts
{
    using System;
    using CFH.Models;

    public interface IConditioData : IDisposable
    {
        IGenericRepository<File> Files { get; }

        IGenericRepository<Directory> Directories { get; }

        IGenericRepository<Condition> Conditions { get; }

        int SaveChanges();
    }
}
