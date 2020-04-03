namespace CookingBook.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;

    public class RecipeSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Recipes.Any())
            {
                return;
            }
            await dbContext.Recipes.AddAsync(new Recipe()
            {
                Id = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.Now,
                Category = dbContext.Categories.FirstOrDefault(x => x.Title == "Dessert"),
                Title = "Blueberry Cheesecake",
                Photo = "https://vaya.in/recipes/wp-content/uploads/2018/12/Blueberry-Cheesecake..jpg",
                CookTime = 120,
                Serving = 12,
                UserId = "22e1f0c0-a6ff-4864-b939-b0c570f2d51d",
                NutritionValue = new NutritionValue() { Calories = 356, Fats = 24, Carbohydrates = 32, Fiber = 5, Protein = 0, Salt = 0, Sugar = 60 },
                Products = new Product()
                {
                    Product1 = "1 cup graham cracker crumbs",
                    Product2 = "2 tbsps white sugar",
                    Product3 = "1/4 cup melted butter",
                    Product4 = "16 oz cream cheese – softened",
                    Product5 = "1 cup sour cream",
                    Product6 = "3/4 cup white sugar",
                    Product7 = "1 tsp vanilla extract",
                    Product8 = "2 tbsps all-purpose flour 4 eggs",
                    Product9 = "2 cups frozen blueberries",
                    Product10 = "1/3 cup blueberry jelly",
                },
                CookProcedure = "Combine the butter, crumbs, and 2 tablespoons sugar.Pat mixture into the bottom of a 9-inch springform pan.Mash the cream cheese until it turns soft and creamy.Gradually beat in ¾ cup sugar, sour cream, flour, and vanilla.Beat in eggs one at a time.Pour the mixture into crumb-lined pan.Bake in an oven for 1 hour at 325 °F oven until the crust is firm to the touch.Let cool.Remove the cake from the pan by lifting the edges with a knife.Place frozen blueberries on top of the cake.Melt the jelly and spoon it over the blueberries as a glaze. Chill and serve.",
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
