namespace CFH.Data
{
    using System.Data.Entity;
    using CFH.Data.Contracts;
    using CFH.Data.Migrations;
    using CFH.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, ICFHDbContext
    {

        public ApplicationDbContext()
            : base(@"name=""ConnectionStringName""providerName=""System.Data.SqlClient""connectionString=""Data Source=.\SQLEXPRESS;Initial Catalog=CFH;Integrated Security=True;MultipleActiveResultSets=True", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public IDbSet<File> Files { get; set; }

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
