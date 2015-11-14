namespace CFH.Data.Migrations
{
    using Models;
    using Contracts;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            this.SeedDirectories(context);
        }

        private void SeedDirectories(ICFHDbContext context)
        {
            context.SaveChanges();
        }
    }
}
