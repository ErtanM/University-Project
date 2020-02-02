namespace GalleryApp.Data.Migrations
{
    using GalleryApp.Data.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GalleryApp.Data.Database>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GalleryApp.Data.Database context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
         

            try
            {

                context.Marka.AddOrUpdate(p => p.Name, new Marka() { Name = "Audi" });
                context.Marka.AddOrUpdate(p => p.Name, new Marka() { Name = "Mercedes" });
                context.Marka.AddOrUpdate(p => p.Name, new Marka() { Name = "BMW" });
                context.Marka.AddOrUpdate(p => p.Name, new Marka() { Name = "Bentley" });
                context.Marka.AddOrUpdate(p => p.Name, new Marka() { Name = "Ferrari" });
                context.Marka.AddOrUpdate(p => p.Name, new Marka() { Name = "Tesla" });

                context.SaveChanges();

                var audi = context.Marka.FirstOrDefault(f => f.Name == "Audi");
                if(audi != null)
                {
                    // AUDI                          
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "A4", MarkaID = audi.ID });
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "A6", MarkaID = audi.ID });
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "TT", MarkaID = audi.ID });
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "Q5", MarkaID = audi.ID });
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "A3", MarkaID = audi.ID });
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "R8", MarkaID = audi.ID });
                }

                var mercedes = context.Marka.FirstOrDefault(f => f.Name == "Mercedes");
                if (mercedes != null)
                {
                    //Mercedes                       
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "E Class", MarkaID = mercedes.ID });
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "C Class", MarkaID = mercedes.ID });
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "S Class", MarkaID = mercedes.ID });
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "SLS AMG", MarkaID = mercedes.ID });
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "AMG GT", MarkaID = mercedes.ID });
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "G Class", MarkaID = mercedes.ID });
                }

                var bmw = context.Marka.FirstOrDefault(f => f.Name == "BMW");
                if (bmw != null)
                {
                    //BMW                            
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "8-Series", MarkaID = bmw.ID });
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "X1", MarkaID = bmw.ID });
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "5-Series", MarkaID = bmw.ID });
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "6-Series", MarkaID = bmw.ID });
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "2-Series", MarkaID = bmw.ID });
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "Z4", MarkaID = bmw.ID });
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "X5", MarkaID = bmw.ID });
                }

                var bentley = context.Marka.FirstOrDefault(f => f.Name == "Bentley");
                if (bentley != null)
                {
                    //Bentley                        
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "Flying Spur", MarkaID = bentley.ID });
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "Azure", MarkaID = bentley.ID });
                }

                var ferrari = context.Marka.FirstOrDefault(f => f.Name == "Ferrari");
                if (ferrari != null)
                {                     
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "F40", MarkaID = ferrari.ID });
                }

                var tesla = context.Marka.FirstOrDefault(f => f.Name == "Tesla");
                if (tesla != null)
                {
                    //Tesla                          
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "Model S", MarkaID = tesla.ID });
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "Model 3", MarkaID = tesla.ID });
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "Model X", MarkaID = tesla.ID });
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "Model Y", MarkaID = tesla.ID });
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "Cyber Truck", MarkaID = tesla.ID });
                    context.Model.AddOrUpdate(p => p.Name, new Model() { Name = "Model Roadster", MarkaID = tesla.ID });
                }

            }
            catch (DbUpdateException e)
            {
                throw e;
                
            }
            catch (Exception dbEx)
            {
                throw dbEx;
            }



        }
    }
}
