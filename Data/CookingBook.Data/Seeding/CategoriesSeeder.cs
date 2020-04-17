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

            await dbContext.AddAsync(new Category { Title = "Salads", Image = "https://i.ibb.co/nPWQ0YG/salad-banner.jpg" });
            await dbContext.AddAsync(new Category { Title = "Soups", Image = "https://i.ibb.co/kg6vNDf/soups.jpg" });
            await dbContext.AddAsync(new Category { Title = "Beef", Image = "https://i.ibb.co/m5ZqjNG/beef.jpg" });
            await dbContext.AddAsync(new Category { Title = "Chicken", Image = "https://i.ibb.co/1s1wtH6/chicken.jpg" });
            await dbContext.AddAsync(new Category { Title = "Pork", Image = "https://i.ibb.co/D589CjX/pork.jpg" });
            await dbContext.AddAsync(new Category { Title = "Fish", Image = "https://i.ibb.co/wSkQtC1/fish.jpg" });
            await dbContext.AddAsync(new Category { Title = "Pizza", Image = "https://i.ibb.co/R4NrD33/pizza.jpg" });
            await dbContext.AddAsync(new Category { Title = "Grill", Image = "https://i.ibb.co/tDfb8B5/grill.jpg" });
            await dbContext.AddAsync(new Category { Title = "Breads", Image = "https://i.ibb.co/sjNmJ7P/breads.jpg" });
            await dbContext.AddAsync(new Category { Title = "Desserts", Image = "https://i.ibb.co/5BJ1Pmr/desserts.jpg" });
        }
    }
}
