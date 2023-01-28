namespace ConsultoriaSAS.Data.Migrations
{
    using ConsultoriaSAS.Data.Context;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

     public  /*internal*/ sealed class Configuration : DbMigrationsConfiguration<ConsultoriaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            //ContextKey = "ConsultoriaSAS.Data.Migrations.Configuration";
        }

        protected override void Seed(ConsultoriaContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
