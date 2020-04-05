namespace CookingBook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CookingBook.Data.Common.Repositories;
    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;
    using CookingBook.Web.ViewModels.Recipes;
    using Microsoft.EntityFrameworkCore.Migrations.Operations;
    using Microsoft.Extensions.Primitives;

    public class RecipesService : IRecipesService
    {
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly IDeletableEntityRepository<Recipe> recipeRepository;
        private readonly IDeletableEntityRepository<NutritionValue> nutritionRepository;

        public RecipesService(
            IDeletableEntityRepository<Recipe> recipeRepository,
            IDeletableEntityRepository<NutritionValue> nutritionRepository,
            IDeletableEntityRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
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

        public IEnumerable<T> GetByCategoryId<T>(int categoryId)
        {
            return this.recipeRepository.All().Where(x => x.CategoryId == categoryId).Where(x => x.IsDeleted == false).To<T>().ToList();
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
            await this.recipeRepository.SaveChangesAsync();

            for (int i = 0; i < sessionKeysList.Count; i++)
            {
                await this.productRepository.AddAsync(new Product
                {
                    Id = Guid.NewGuid().ToString(),
                    RecipeId = id,
                    Name = sessionKeysList[i].ToString(),
                    Quantity = double.Parse(sessionValuesList[i]),
                });
            }

            await this.productRepository.SaveChangesAsync();

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
            await this.nutritionRepository.SaveChangesAsync();
            
            return id;
        }
    }
}
