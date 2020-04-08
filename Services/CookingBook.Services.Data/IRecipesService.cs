﻿namespace CookingBook.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CookingBook.Data.Models;
    using CookingBook.Web.ViewModels.Recipes;
    using Microsoft.Extensions.Primitives;

    public interface IRecipesService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetByCategoryId<T>(int categoryId);

        T GetById<T>(string recipeId);

        Task<string> CreateAsync(RecipeCreateViewModel model, string userId, StringValues sessionKeysList, StringValues sessionValuesList);

        Task<string> AddReview(ReviewForRecipeViewModel model);

        Task<int> CookRecipe(string recipeId, string userId);

        Task<string> AddToFavorites(string recipeId, string userId);

        Task SoftDelete(string id);
    }
}
