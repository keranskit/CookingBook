namespace CookingBook.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Models;

    internal class AllergensSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Allergens.Any())
            {
                return;
            }

            await dbContext.Allergens.AddAsync(new Allergen { Name = "Gluten" });
            await dbContext.Allergens.AddAsync(new Allergen { Name = "Crustaceans" });
            await dbContext.Allergens.AddAsync(new Allergen { Name = "Eggs" });
            await dbContext.Allergens.AddAsync(new Allergen { Name = "Fish" });
            await dbContext.Allergens.AddAsync(new Allergen { Name = "Peanuts" });
            await dbContext.Allergens.AddAsync(new Allergen { Name = "Soybeans" });
            await dbContext.Allergens.AddAsync(new Allergen { Name = "Milk" });
            await dbContext.Allergens.AddAsync(new Allergen { Name = "Nuts" });
            await dbContext.Allergens.AddAsync(new Allergen { Name = "Celery" });
            await dbContext.Allergens.AddAsync(new Allergen { Name = "Mustard" });
            await dbContext.Allergens.AddAsync(new Allergen { Name = "Sesame" });
            await dbContext.Allergens.AddAsync(new Allergen { Name = "Sulphites" });
            await dbContext.Allergens.AddAsync(new Allergen { Name = "Lupin" });
            await dbContext.Allergens.AddAsync(new Allergen { Name = "Molluscs" });
        }
    }
}
