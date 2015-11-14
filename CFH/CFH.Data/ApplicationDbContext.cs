namespace CFH.Data
{
    using System.Data.Entity;
    using CFH.Common;
    using CFH.Data.Contracts;
    using CFH.Data.Migrations;
    using CFH.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IConditioDataContext
    {

        public ApplicationDbContext()
            : base(ConnectionStrings.DefaultConnection, throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public IDbSet<Directory> Directories { get; set; }

        public IDbSet<File> Files { get; set; }

        public IDbSet<Condition> Conditions { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
