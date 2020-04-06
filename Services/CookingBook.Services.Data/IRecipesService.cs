namespace CookingBook.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Primitives;
    using CookingBook.Web.ViewModels.Recipes;

    public interface IRecipesService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetByCategoryId<T>(int categoryId);

        T GetById<T>(string recipeId);

        Task<string> CreateAsync(RecipeCreateViewModel model, string userId, StringValues sessionKeysList, StringValues sessionValuesList);

        Task<string> AddReview(ReviewForRecipeViewModel model);

        Task<string> CookRecipe(string recipeId, string userId);

        Task<string> AddToFavorites(string recipeId, string userId);
    }
}