namespace CFH.Data.Contracts
{
    using System.Data.Entity;
    using CFH.Models;

    public interface ICFHDbContext : IDbContext
    {
        IDbSet<File> Files { get; set; }

        IDbSet<ApplicationUser> Users { get; set; }
    }
}
