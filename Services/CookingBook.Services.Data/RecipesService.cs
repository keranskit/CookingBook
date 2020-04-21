namespace CookingBook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using CookingBook.Data.Common.Repositories;
    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;
    using CookingBook.Web.ViewModels.Administration.Main;
    using CookingBook.Web.ViewModels.Profile;
    using CookingBook.Web.ViewModels.Recipes;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Primitives;

    public class RecipesService : IRecipesService
    {
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<Recipe> recipeRepository;
        private readonly IDeletableEntityRepository<NutritionValue> nutritionRepository;

        public RecipesService(
            IDeletableEntityRepository<Recipe> recipeRepository,
            IDeletableEntityRepository<NutritionValue> nutritionRepository,
            IDeletableEntityRepository<Product> productRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.productRepository = productRepository;
            this.userRepository = userRepository;
            this.recipeRepository = recipeRepository;
            this.nutritionRepository = nutritionRepository;
        }

        public int GetCount()
        {
            return this.recipeRepository.All().Count();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.recipeRepository.All().Where(x => x.IsDeleted == false).To<T>().ToList();
        }

        public IEnumerable<T> GetNewestFiveRecipes<T>()
        {
            return this.recipeRepository.All().OrderByDescending(x => x.CreatedOn).To<T>().Take(6).ToList();
        }

        public IEnumerable<T> GetByCategoryId<T>(int categoryId)
        {
            return this.recipeRepository.All().Where(x => x.CategoryId == categoryId).OrderByDescending(x => x.CreatedOn).To<T>().ToList();
        }

        public T GetById<T>(string recipeId)
        {
            var recipe = this.recipeRepository.All().Where(x => x.Id == recipeId && x.IsDeleted == false)
                .To<T>().FirstOrDefault();

            return recipe;
        }

        public async Task<string> CreateAsync(RecipeCreateViewModel model, string userId, StringValues sessionKeysList, StringValues sessionValuesList)
        {
            var id = Guid.NewGuid().ToString();
            await this.recipeRepository.AddAsync(new Recipe
            {
                Id = id,
                CategoryId = model.CategoryId,
                Title = model.SanitizedTitle,
                CookProcedure = model.SanitizedCookProcedure,
                CookTime = model.CookTime,
                Serving = model.Serving,
                Photo = model.Photo,
                NutritionValueId = id,
                UserId = userId,
                CreatedOn = DateTime.UtcNow,
            });

            bool isCorrectlyParsed = false;

            for (int i = 0; i < sessionKeysList.Count; i++)
            {
                isCorrectlyParsed = decimal.TryParse(sessionValuesList[i], NumberStyles.Number, CultureInfo.InvariantCulture, out var parsed);
                await this.productRepository.AddAsync(new Product
                {
                    Id = Guid.NewGuid().ToString(),
                    RecipeId = id,
                    Name = sessionKeysList[i].ToString(),
                    Quantity = parsed,
                });
            }

            await this.nutritionRepository.AddAsync(new NutritionValue
            {
                Id = Guid.NewGuid().ToString(),
                RecipeId = id,
                Calories = model.NutritionValue.Calories,
                Carbohydrates = model.NutritionValue.Carbohydrates,
                Fats = model.NutritionValue.Fats,
                Fiber = model.NutritionValue.Fiber,
                Protein = model.NutritionValue.Protein,
                Salt = model.NutritionValue.Salt,
                Sugar = model.NutritionValue.Sugar,
                CreatedOn = DateTime.UtcNow,
            });

            if (isCorrectlyParsed)
            {
                await this.recipeRepository.SaveChangesAsync();
                await this.productRepository.SaveChangesAsync();
                await this.nutritionRepository.SaveChangesAsync();
            }

            return id;
        }

        public async Task<string> AddReview(ReviewForRecipeViewModel model)
        {
            this.recipeRepository.All().FirstOrDefault(x => x.Id == model.RecipeId).Reviews.Add(new Review
            {
                Comment = model.SanitizedReview,
                Score = model.Score,
                RecipeId = model.RecipeId,
                UserId = model.UserId,
            });
            await this.recipeRepository.SaveChangesAsync();

            return model.RecipeId;
        }

        public async Task<int> CookRecipe(string recipeId, string userId)
        {
            var recipe = this.recipeRepository.All().Include(cooked => cooked.CookedBy).FirstOrDefault(x => x.Id == recipeId);
            bool isCooked = false;
            foreach (var pair in recipe.CookedBy)
            {
                if (pair.RecipeId == recipeId && pair.UserId == userId)
                {
                    isCooked = true;
                }
            }

            if (isCooked)
            {
                return -1;
            }
            else
            {
                recipe.CookedBy.Add(new UserCookedRecipe
                {
                    RecipeId = recipeId,
                    UserId = userId,
                });

                this.recipeRepository.Update(recipe);
                await this.recipeRepository.SaveChangesAsync();
            }

            return recipe.CookedBy.Count();
        }

        public async Task<int> AddToFavorites(string recipeId, string userId)
        {
            var recipe = this.recipeRepository.All().Include(cooked => cooked.FavoriteBy).FirstOrDefault(x => x.Id == recipeId);
            bool isFavorited = false;
            foreach (var pair in recipe.FavoriteBy)
            {
                if (pair.RecipeId == recipeId && pair.UserId == userId)
                {
                    isFavorited = true;
                }
            }

            if (isFavorited)
            {
                return -1;
            }

            recipe.FavoriteBy.Add(new UserFavoriteRecipe
            {
                RecipeId = recipeId,
                UserId = userId,
            });
            this.recipeRepository.Update(recipe);
            await this.recipeRepository.SaveChangesAsync();

            return 1;
        }

        public async Task SoftDelete(string id)
        {
            var recipe = await this.recipeRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            recipe.IsDeleted = true;
            recipe.DeletedOn = DateTime.Now;
            this.recipeRepository.Update(recipe);
            await this.recipeRepository.SaveChangesAsync();
        }

        // Only the creator can edit his recipe.
        public async Task EditRecipe(RecipeEditViewModel model, string userId)
        {
            var recipe = await this.recipeRepository.All().FirstOrDefaultAsync(x => x.Id == model.Id);
            if (recipe.UserId == userId)
            {
                recipe.CategoryId = model.CategoryId;
                recipe.CookProcedure = model.SanitizedCookProcedure;
                recipe.Title = model.Title;
                recipe.CookTime = model.CookTime;
                recipe.Photo = model.Photo;
                recipe.Serving = model.Serving;
                recipe.ModifiedOn = DateTime.UtcNow;
                this.recipeRepository.Update(recipe);
            }

            await this.recipeRepository.SaveChangesAsync();
        }

        public async Task EditByAdmin(AdminRecipeViewModel model)
        {
            var recipe = await this.recipeRepository.All().FirstOrDefaultAsync(x => x.Id == model.Id);

            recipe.CategoryId = model.CategoryId;
            recipe.CookProcedure = model.SanitizedCookProcedure;
            recipe.Title = model.Title;
            recipe.CookTime = model.CookTime;
            recipe.Photo = model.Photo;
            recipe.Serving = model.Serving;
            recipe.ModifiedOn = DateTime.UtcNow;
            this.recipeRepository.Update(recipe);

            await this.recipeRepository.SaveChangesAsync();
        }
    }
}
