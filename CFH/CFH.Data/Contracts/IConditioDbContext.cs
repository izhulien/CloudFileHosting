namespace CFH.Data.Contracts
{
    using System.Data.Entity;
    using CFH.Models;

    public interface IConditioDataContext : IDbContext
    {
        IDbSet<File> Files { get; set; }

        IDbSet<Directory> Directories { get; set; }

        IDbSet<Condition> Conditions { get; set; }
    }
}
