namespace CookingBook.Services.Data.Tests
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using CookingBook.Data;
    using CookingBook.Data.Models;
    using CookingBook.Data.Repositories;
    using CookingBook.Services.Mapping;
    using CookingBook.Web.ViewModels.Categories;
    using CookingBook.Web.ViewModels.Recipes;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class RecipesServiceTests
    {
        [Fact]
        public async Task GetCountShouldReturnProperCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);
            dbContext.Recipes.Add(new Recipe());
            dbContext.Recipes.Add(new Recipe());
            dbContext.Recipes.Add(new Recipe());
            await dbContext.SaveChangesAsync();
            var recipeRepo = new EfDeletableEntityRepository<Recipe>(dbContext);
            var nutritionRepo = new EfDeletableEntityRepository<NutritionValue>(dbContext);
            var productRepo = new EfDeletableEntityRepository<Product>(dbContext);
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var service = new RecipesService(recipeRepo, nutritionRepo, productRepo, userRepo);

            Assert.Equal(3, service.GetCount());
        }

        [Fact]
        public async Task GetAllShouldReturnCollection()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);
            dbContext.Recipes.Add(new Recipe());
            dbContext.Recipes.Add(new Recipe());
            await dbContext.SaveChangesAsync();
            var recipeRepo = new EfDeletableEntityRepository<Recipe>(dbContext);
            var nutritionRepo = new EfDeletableEntityRepository<NutritionValue>(dbContext);
            var productRepo = new EfDeletableEntityRepository<Product>(dbContext);
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var service = new RecipesService(recipeRepo, nutritionRepo, productRepo, userRepo);

            Assert.Equal(2, service.GetAll<RecipeInCategoryViewModel>().Count());
        }

        [Fact]
        public async Task GetAllShouldReturnNewestFiveRecipes()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);
            
            var category = new Category();
            var nutrValue = new NutritionValue();
            var user = new ApplicationUser();
            var prod = new Collection<RecipeByIdProductsViewModel>();
            dbContext.Recipes.Add(new Recipe { Id = "a", CreatedOn = DateTime.Parse("2008-11-01T19:31:00.0000000Z"), Title = "newTitle1", NutritionValueId = "a", CategoryId = 1, CookProcedure = "a", CookTime = 1, Photo = "a", Serving = 1, UserId = "az", Category = category, NutritionValue = nutrValue, User = user });
            dbContext.Recipes.Add(new Recipe { Id = "b", CreatedOn = DateTime.Parse("2008-11-01T19:32:00.0000000Z"), Title = "newTitle2", NutritionValueId = "a", CategoryId = 1, CookProcedure = "a", CookTime = 1, Photo = "a", Serving = 1, UserId = "az", Category = category, NutritionValue = nutrValue, User = user });
            dbContext.Recipes.Add(new Recipe { Id = "c", CreatedOn = DateTime.Parse("2008-11-01T19:33:00.0000000Z"), Title = "newTitle3", NutritionValueId = "a", CategoryId = 1, CookProcedure = "a", CookTime = 1, Photo = "a", Serving = 1, UserId = "az", Category = category, NutritionValue = nutrValue, User = user });
            dbContext.Recipes.Add(new Recipe { Id = "d", CreatedOn = DateTime.Parse("2008-11-01T19:34:00.0000000Z"), Title = "newTitle4", NutritionValueId = "a", CategoryId = 1, CookProcedure = "a", CookTime = 1, Photo = "a", Serving = 1, UserId = "az", Category = category, NutritionValue = nutrValue, User = user });
            dbContext.Recipes.Add(new Recipe { Id = "e", CreatedOn = DateTime.Parse("2008-11-01T19:35:00.0000000Z"), Title = "newTitle5", NutritionValueId = "a", CategoryId = 1, CookProcedure = "a", CookTime = 1, Photo = "a", Serving = 1, UserId = "az", Category = category, NutritionValue = nutrValue, User = user });
            dbContext.Recipes.Add(new Recipe { Id = "f", CreatedOn = DateTime.Parse("2008-11-01T19:36:00.0000000Z"), Title = "newTitle6", NutritionValueId = "a", CategoryId = 1, CookProcedure = "a", CookTime = 1, Photo = "a", Serving = 1, UserId = "az", Category = category, NutritionValue = nutrValue, User = user });
            dbContext.Recipes.Add(new Recipe { Id = "g", CreatedOn = DateTime.Parse("2008-11-01T19:37:00.0000000Z"), Title = "newTitle7", NutritionValueId = "a", CategoryId = 1, CookProcedure = "a", CookTime = 1, Photo = "a", Serving = 1, UserId = "az", Category = category, NutritionValue = nutrValue, User = user });
            dbContext.Recipes.Add(new Recipe { Id = "h", CreatedOn = DateTime.Parse("2008-11-01T19:38:00.0000000Z"), Title = "newTitle8", NutritionValueId = "a", CategoryId = 1, CookProcedure = "a", CookTime = 1, Photo = "a", Serving = 1, UserId = "az", Category = category, NutritionValue = nutrValue, User = user });
            await dbContext.SaveChangesAsync();
            
            var recipeRepo = new EfDeletableEntityRepository<Recipe>(dbContext);
            var nutritionRepo = new EfDeletableEntityRepository<NutritionValue>(dbContext);
            var productRepo = new EfDeletableEntityRepository<Product>(dbContext);
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var service = new RecipesService(recipeRepo, nutritionRepo, productRepo, userRepo);

            Assert.Equal(6, service.GetNewestFiveRecipes<RecipeDTO>().Count());
        }
    }
}
