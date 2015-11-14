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

        private void SeedDirectories(IConditioDbContext context)
        {
            if (context.Directories.Any())
            {
                return;
            }

            context.Directories.Add(new Directory()
            {
                Name = "Books"
            });

            context.Directories.Add(new Directory()
            {
                Name = "Programs"
            });

            context.Directories.Add(new Directory()
            {
                Name = "Pictures"
            });

            context.Directories.Add(new Directory()
            {
                Name = "Video"
            });

            context.Directories.Add(new Directory()
            {
                Name = "Audio"
            });

            context.SaveChanges();
        }
    }
}
