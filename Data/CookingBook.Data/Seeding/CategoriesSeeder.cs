namespace CookingBook.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CookingBook.Data.Models;

    internal class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.AddAsync(new Category { Title = "Salad" });
            await dbContext.AddAsync(new Category { Title = "Soup" });
            await dbContext.AddAsync(new Category { Title = "Beef" });
            await dbContext.AddAsync(new Category { Title = "Chicken" });
            await dbContext.AddAsync(new Category { Title = "Pork" });
            await dbContext.AddAsync(new Category { Title = "Fish" });
            await dbContext.AddAsync(new Category { Title = "Pizza" });
            await dbContext.AddAsync(new Category { Title = "Grill" });
            await dbContext.AddAsync(new Category { Title = "Breads" });
            await dbContext.AddAsync(new Category { Title = "Dessert" });
        }
    }
}
