namespace CFH.Data.Contracts
{
    using System.Data.Entity;
    using CFH.Models;

    public interface IConditioDbContext : IDbContext
    {
        IDbSet<File> Files { get; set; }

        IDbSet<Directory> Directories { get; set; }

        IDbSet<Condition> Conditions { get; set; }
    }
}
