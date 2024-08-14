namespace Nhom4_DeTai7.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Nhom4_DeTai7.Models.QL_WEB_BANHNGOTContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Nhom4_DeTai7.Models.QL_WEB_BANHNGOTContext";
        }

        protected override void Seed(Nhom4_DeTai7.Models.QL_WEB_BANHNGOTContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
