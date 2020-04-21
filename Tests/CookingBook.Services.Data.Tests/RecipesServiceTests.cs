namespace CookingBook.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using CookingBook.Data;
    using CookingBook.Data.Models;
    using CookingBook.Data.Repositories;
    using CookingBook.Services.Mapping;
    using CookingBook.Web.ViewModels.Administration.Main;
    using CookingBook.Web.ViewModels.Categories;
    using CookingBook.Web.ViewModels.Profile;
    using CookingBook.Web.ViewModels.Recipes;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Primitives;
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
            dbContext.Recipes.Add(new Recipe
            {
                Id = "a", CreatedOn = DateTime.Parse("2008-11-01T19:31:00.0000000Z"), Title = "newTitle1",
                NutritionValueId = "a", CategoryId = 1, CookProcedure = "a", CookTime = 1, Photo = "a", Serving = 1,
                UserId = "az", Category = category, NutritionValue = nutrValue, User = user,
            });
            dbContext.Recipes.Add(new Recipe
            {
                Id = "b", CreatedOn = DateTime.Parse("2008-11-01T19:32:00.0000000Z"), Title = "newTitle2",
                NutritionValueId = "a", CategoryId = 1, CookProcedure = "a", CookTime = 1, Photo = "a", Serving = 1,
                UserId = "az", Category = category, NutritionValue = nutrValue, User = user,
            });
            dbContext.Recipes.Add(new Recipe
            {
                Id = "c", CreatedOn = DateTime.Parse("2008-11-01T19:33:00.0000000Z"), Title = "newTitle3",
                NutritionValueId = "a", CategoryId = 1, CookProcedure = "a", CookTime = 1, Photo = "a", Serving = 1,
                UserId = "az", Category = category, NutritionValue = nutrValue, User = user,
            });
            dbContext.Recipes.Add(new Recipe
            {
                Id = "d", CreatedOn = DateTime.Parse("2008-11-01T19:34:00.0000000Z"), Title = "newTitle4",
                NutritionValueId = "a", CategoryId = 1, CookProcedure = "a", CookTime = 1, Photo = "a", Serving = 1,
                UserId = "az", Category = category, NutritionValue = nutrValue, User = user,
            });
            dbContext.Recipes.Add(new Recipe
            {
                Id = "e", CreatedOn = DateTime.Parse("2008-11-01T19:35:00.0000000Z"), Title = "newTitle5",
                NutritionValueId = "a", CategoryId = 1, CookProcedure = "a", CookTime = 1, Photo = "a", Serving = 1,
                UserId = "az", Category = category, NutritionValue = nutrValue, User = user,
            });
            dbContext.Recipes.Add(new Recipe
            {
                Id = "f", CreatedOn = DateTime.Parse("2008-11-01T19:36:00.0000000Z"), Title = "newTitle6",
                NutritionValueId = "a", CategoryId = 1, CookProcedure = "a", CookTime = 1, Photo = "a", Serving = 1,
                UserId = "az", Category = category, NutritionValue = nutrValue, User = user,
            });
            dbContext.Recipes.Add(new Recipe
            {
                Id = "g", CreatedOn = DateTime.Parse("2008-11-01T19:37:00.0000000Z"), Title = "newTitle7",
                NutritionValueId = "a", CategoryId = 1, CookProcedure = "a", CookTime = 1, Photo = "a", Serving = 1,
                UserId = "az", Category = category, NutritionValue = nutrValue, User = user,
            });
            dbContext.Recipes.Add(new Recipe
            {
                Id = "h", CreatedOn = DateTime.Parse("2008-11-01T19:38:00.0000000Z"), Title = "newTitle8",
                NutritionValueId = "a", CategoryId = 1, CookProcedure = "a", CookTime = 1, Photo = "a", Serving = 1,
                UserId = "az", Category = category, NutritionValue = nutrValue, User = user,
            });
            await dbContext.SaveChangesAsync();

            var recipeRepo = new EfDeletableEntityRepository<Recipe>(dbContext);
            var nutritionRepo = new EfDeletableEntityRepository<NutritionValue>(dbContext);
            var productRepo = new EfDeletableEntityRepository<Product>(dbContext);
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var service = new RecipesService(recipeRepo, nutritionRepo, productRepo, userRepo);

            Assert.Equal(6, service.GetNewestFiveRecipes<RecipeDTO>().Count());
        }

        [Fact]
        public async Task GetByCategoryIdShouldReturnOnlyTheCategory()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);

            var category = new Category();
            var nutrValue = new NutritionValue();
            var user = new ApplicationUser();
            var prod = new Collection<RecipeByIdProductsViewModel>();
            dbContext.Recipes.Add(new Recipe
            {
                Id = "a", CreatedOn = DateTime.Parse("2008-11-01T19:31:00.0000000Z"), Title = "newTitle1",
                CategoryId = 1,
            });
            dbContext.Recipes.Add(new Recipe
            {
                Id = "b", CreatedOn = DateTime.Parse("2008-11-01T19:32:00.0000000Z"), Title = "newTitle2",
                CategoryId = 1,
            });
            dbContext.Recipes.Add(new Recipe
            {
                Id = "c", CreatedOn = DateTime.Parse("2008-11-01T19:33:00.0000000Z"), Title = "newTitle3",
                CategoryId = 2,
            });
            dbContext.Recipes.Add(new Recipe
            {
                Id = "d", CreatedOn = DateTime.Parse("2008-11-01T19:34:00.0000000Z"), Title = "newTitle4",
                CategoryId = 3,
            });
            dbContext.Recipes.Add(new Recipe
            {
                Id = "e", CreatedOn = DateTime.Parse("2008-11-01T19:35:00.0000000Z"), Title = "newTitle5",
                CategoryId = 4,
            });
            dbContext.Recipes.Add(new Recipe
            {
                Id = "f", CreatedOn = DateTime.Parse("2008-11-01T19:36:00.0000000Z"), Title = "newTitle6",
                CategoryId = 4,
            });
            dbContext.Recipes.Add(new Recipe
            {
                Id = "g", CreatedOn = DateTime.Parse("2008-11-01T19:37:00.0000000Z"), Title = "newTitle7",
                CategoryId = 4,
            });
            dbContext.Recipes.Add(new Recipe
            {
                Id = "h", CreatedOn = DateTime.Parse("2008-11-01T19:38:00.0000000Z"), Title = "newTitle8",
                CategoryId = 4,
            });
            await dbContext.SaveChangesAsync();

            var recipeRepo = new EfDeletableEntityRepository<Recipe>(dbContext);
            var nutritionRepo = new EfDeletableEntityRepository<NutritionValue>(dbContext);
            var productRepo = new EfDeletableEntityRepository<Product>(dbContext);
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var service = new RecipesService(recipeRepo, nutritionRepo, productRepo, userRepo);

            Assert.Equal(4, service.GetByCategoryId<RecipeDTO>(4).Count());
        }

        [Fact]
        public async Task GetByIdShouldReturnTheExpectedType()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);

            var category = new Category();
            var nutrValue = new NutritionValue();
            var user = new ApplicationUser();
            var prod = new Collection<RecipeByIdProductsViewModel>();
            dbContext.Recipes.Add(new Recipe
            {
                Id = "a", CreatedOn = DateTime.Parse("2008-11-01T19:31:00.0000000Z"), Title = "newTitle1",
                CategoryId = 1,
            });
            dbContext.Recipes.Add(new Recipe
            {
                Id = "b", CreatedOn = DateTime.Parse("2008-11-01T19:32:00.0000000Z"), Title = "newTitle2",
                CategoryId = 1,
            });
            dbContext.Recipes.Add(new Recipe
            {
                Id = "c", CreatedOn = DateTime.Parse("2008-11-01T19:33:00.0000000Z"), Title = "newTitle3",
                CategoryId = 2,
            });
            await dbContext.SaveChangesAsync();

            var recipeRepo = new EfDeletableEntityRepository<Recipe>(dbContext);
            var nutritionRepo = new EfDeletableEntityRepository<NutritionValue>(dbContext);
            var productRepo = new EfDeletableEntityRepository<Product>(dbContext);
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var service = new RecipesService(recipeRepo, nutritionRepo, productRepo, userRepo);

            Assert.IsType<RecipeDTO>(service.GetById<RecipeDTO>("a"));
            Assert.IsNotType<RecipeDTO>(service.GetById<RecipeDTO>("asd"));
        }

        [Fact]
        public async Task CreateRecipeShouldAddRecipeSuccessfully()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);

            var category = new Category();
            var nutrValue = new NutritionValue();
            var user = new ApplicationUser();
            var prod = new Collection<RecipeByIdProductsViewModel>();
            var recipeCreateViewModel = new RecipeCreateViewModel
            {
                CategoryId = 1,
                CookProcedure = "cookProc",
                Photo = "photo",
                Serving = 1,
                Title = "addNew",
                CookTime = 2,
                NutritionValue = new RecipeCreateNutritionValuesViewModel
                    { Calories = 1, Carbohydrates = 1, Fats = 1, Fiber = 1, Protein = 1, Salt = 1, Sugar = 1, },
                Products = new List<RecipeCreateProductsViewModel>(),
            };

            string userId = "trayan";
            StringValues sv = new StringValues("one");
            StringValues sk = new StringValues("1");

            var recipeRepo = new EfDeletableEntityRepository<Recipe>(dbContext);
            var nutritionRepo = new EfDeletableEntityRepository<NutritionValue>(dbContext);
            var productRepo = new EfDeletableEntityRepository<Product>(dbContext);
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var service = new RecipesService(recipeRepo, nutritionRepo, productRepo, userRepo);

            var result = await service.CreateAsync(recipeCreateViewModel, userId, sv, sk);

            Assert.IsType<string>(result);
            Assert.NotEmpty(result);
            Assert.NotNull(result);
            Assert.True(dbContext.Recipes.Any(x => x.Id == result));
            Assert.Equal(1, dbContext.Recipes.Count());
        }

        [Fact]
        public async Task AddReviewShouldCreateANewReviewToRecipe()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);

            var recipeRepo = new EfDeletableEntityRepository<Recipe>(dbContext);
            var nutritionRepo = new EfDeletableEntityRepository<NutritionValue>(dbContext);
            var productRepo = new EfDeletableEntityRepository<Product>(dbContext);
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var service = new RecipesService(recipeRepo, nutritionRepo, productRepo, userRepo);
            var category = new Category();
            var nutrValue = new NutritionValue();
            var user = new ApplicationUser();
            var prod = new Collection<RecipeByIdProductsViewModel>();
            var recipeCreateViewModel = new RecipeCreateViewModel
            {
                CategoryId = 1,
                CookProcedure = "cookProc",
                Photo = "photo",
                Serving = 1,
                Title = "addNew",
                CookTime = 2,
                NutritionValue = new RecipeCreateNutritionValuesViewModel
                    { Calories = 1, Carbohydrates = 1, Fats = 1, Fiber = 1, Protein = 1, Salt = 1, Sugar = 1, },
                Products = new List<RecipeCreateProductsViewModel>(),
            };
            string userId = "trayan";
            StringValues sv = new StringValues("one");
            StringValues sk = new StringValues("1");
            var recipeResult = await service.CreateAsync(recipeCreateViewModel, userId, sv, sk);
            var model = new ReviewForRecipeViewModel
            {
                Comment = "commentOne",
                RecipeId = recipeResult,
                Score = 5,
                UserId = "trayan",
                CreatedOn = DateTime.Now,
            };
            var reviewResult = await service.AddReview(model);

            Assert.IsType<string>(reviewResult);
            Assert.NotEmpty(reviewResult);
            Assert.NotNull(reviewResult);
            Assert.True(dbContext.Reviews.Any());
        }

        [Fact]
        public async Task SoftDeleteShouldSetIsDeletedToTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);
            var recipeRepo = new EfDeletableEntityRepository<Recipe>(dbContext);
            var nutritionRepo = new EfDeletableEntityRepository<NutritionValue>(dbContext);
            var productRepo = new EfDeletableEntityRepository<Product>(dbContext);
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var service = new RecipesService(recipeRepo, nutritionRepo, productRepo, userRepo);
            var category = new Category();
            var nutrValue = new NutritionValue();
            var user = new ApplicationUser();
            var prod = new Collection<RecipeByIdProductsViewModel>();
            var recipeCreateViewModel = new RecipeCreateViewModel
            {
                CategoryId = 1,
                CookProcedure = "cookProc",
                Photo = "photo",
                Serving = 1,
                Title = "addNew",
                CookTime = 2,
                NutritionValue = new RecipeCreateNutritionValuesViewModel
                    { Calories = 1, Carbohydrates = 1, Fats = 1, Fiber = 1, Protein = 1, Salt = 1, Sugar = 1 },
                Products = new List<RecipeCreateProductsViewModel>(),
            };
            string userId = "trayan";
            StringValues sv = new StringValues("one");
            StringValues sk = new StringValues("1");
            var recipeResult = await service.CreateAsync(recipeCreateViewModel, userId, sv, sk);

            await service.SoftDelete(recipeResult);
            Assert.True(dbContext.Recipes.Find(recipeResult).IsDeleted);
        }

        [Fact]
        public async Task CookRecipeShouldAddEntityToUserCooked()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);
            var recipeRepo = new EfDeletableEntityRepository<Recipe>(dbContext);
            var nutritionRepo = new EfDeletableEntityRepository<NutritionValue>(dbContext);
            var productRepo = new EfDeletableEntityRepository<Product>(dbContext);
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var service = new RecipesService(recipeRepo, nutritionRepo, productRepo, userRepo);
            var category = new Category();
            var nutrValue = new NutritionValue();
            var user = new ApplicationUser();
            var prod = new Collection<RecipeByIdProductsViewModel>();
            var recipeCreateViewModel = new RecipeCreateViewModel
            {
                CategoryId = 1,
                CookProcedure = "cookProc",
                Photo = "photo",
                Serving = 1,
                Title = "addNew",
                CookTime = 2,
                NutritionValue = new RecipeCreateNutritionValuesViewModel
                    { Calories = 1, Carbohydrates = 1, Fats = 1, Fiber = 1, Protein = 1, Salt = 1, Sugar = 1 },
                Products = new List<RecipeCreateProductsViewModel>(),
            };
            string userId = "trayan";
            StringValues sv = new StringValues("one");
            StringValues sk = new StringValues("1");
            var recipeResult = await service.CreateAsync(recipeCreateViewModel, userId, sv, sk);

            var result = await service.CookRecipe(recipeResult, userId);
            Assert.Equal(1, dbContext.UserCookedRecipes.Count());
        }

        [Fact]
        public async Task AddToFavoritesShouldAddEntityToUserFavorites()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);
            var recipeRepo = new EfDeletableEntityRepository<Recipe>(dbContext);
            var nutritionRepo = new EfDeletableEntityRepository<NutritionValue>(dbContext);
            var productRepo = new EfDeletableEntityRepository<Product>(dbContext);
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var service = new RecipesService(recipeRepo, nutritionRepo, productRepo, userRepo);
            var category = new Category();
            var nutrValue = new NutritionValue();
            var user = new ApplicationUser();
            var prod = new Collection<RecipeByIdProductsViewModel>();
            var recipeCreateViewModel = new RecipeCreateViewModel
            {
                CategoryId = 1,
                CookProcedure = "cookProc",
                Photo = "photo",
                Serving = 1,
                Title = "addNew",
                CookTime = 2,
                NutritionValue = new RecipeCreateNutritionValuesViewModel
                { Calories = 1, Carbohydrates = 1, Fats = 1, Fiber = 1, Protein = 1, Salt = 1, Sugar = 1 },
                Products = new List<RecipeCreateProductsViewModel>(),
            };
            string userId = "trayan";
            StringValues sv = new StringValues("one");
            StringValues sk = new StringValues("1");
            var recipeResult = await service.CreateAsync(recipeCreateViewModel, userId, sv, sk);

            var result = await service.AddToFavorites(recipeResult, userId);
            Assert.Equal(1, dbContext.UserFavoriteRecipes.Count());
        }

        [Fact]
        public async Task EditShouldChangeRecipe()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);
            var recipeRepo = new EfDeletableEntityRepository<Recipe>(dbContext);
            var nutritionRepo = new EfDeletableEntityRepository<NutritionValue>(dbContext);
            var productRepo = new EfDeletableEntityRepository<Product>(dbContext);
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var service = new RecipesService(recipeRepo, nutritionRepo, productRepo, userRepo);
            var category = new Category();
            var nutrValue = new NutritionValue();
            var user = new ApplicationUser();
            var prod = new Collection<RecipeByIdProductsViewModel>();
            var recipeCreateViewModel = new RecipeCreateViewModel
            {
                CategoryId = 1,
                CookProcedure = "cookProc",
                Photo = "photo",
                Serving = 1,
                Title = "addNew",
                CookTime = 2,
                NutritionValue = new RecipeCreateNutritionValuesViewModel
                { Calories = 1, Carbohydrates = 1, Fats = 1, Fiber = 1, Protein = 1, Salt = 1, Sugar = 1 },
                Products = new List<RecipeCreateProductsViewModel>(),
            };
            string userId = "trayan";
            StringValues sv = new StringValues("one");
            StringValues sk = new StringValues("1");
            var recipeResult = await service.CreateAsync(recipeCreateViewModel, userId, sv, sk);

            var model = new RecipeEditViewModel
            {
                Id = recipeResult,
                CategoryId = 5,
                CookProcedure = "five",
                CookTime = 5,
                Photo = "fifthPhoto",
                Serving = 5,
                Title = "fifthEdit",
            };

            await service.EditRecipe(model, userId);
            Assert.Equal(5, dbContext.Recipes.FirstOrDefault(x => x.Id == recipeResult).CategoryId);
        }

        [Fact]
        public async Task EditByAdminShouldChangeRecipe()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);
            var recipeRepo = new EfDeletableEntityRepository<Recipe>(dbContext);
            var nutritionRepo = new EfDeletableEntityRepository<NutritionValue>(dbContext);
            var productRepo = new EfDeletableEntityRepository<Product>(dbContext);
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var service = new RecipesService(recipeRepo, nutritionRepo, productRepo, userRepo);
            var category = new Category();
            var nutrValue = new NutritionValue();
            var user = new ApplicationUser();
            var prod = new Collection<RecipeByIdProductsViewModel>();
            var recipeCreateViewModel = new RecipeCreateViewModel
            {
                CategoryId = 1,
                CookProcedure = "cookProc",
                Photo = "photo",
                Serving = 1,
                Title = "addNew",
                CookTime = 2,
                NutritionValue = new RecipeCreateNutritionValuesViewModel
                { Calories = 1, Carbohydrates = 1, Fats = 1, Fiber = 1, Protein = 1, Salt = 1, Sugar = 1 },
                Products = new List<RecipeCreateProductsViewModel>(),
            };
            string userId = "trayan";
            StringValues sv = new StringValues("one");
            StringValues sk = new StringValues("1");
            var recipeResult = await service.CreateAsync(recipeCreateViewModel, userId, sv, sk);

            var model = new AdminRecipeViewModel
            {
                Id = recipeResult,
                CategoryId = 5,
                CookProcedure = "five",
                CookTime = 5,
                Photo = "fifthPhoto",
                Serving = 5,
                Title = "fifthEdit",
            };

            await service.EditByAdmin(model);
            Assert.Equal(5, dbContext.Recipes.FirstOrDefault(x => x.Id == recipeResult).CategoryId);
        }
    }
}
