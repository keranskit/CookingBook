namespace CookingBook.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CookingBook.Web.ViewModels.Administration.Main;
    using CookingBook.Web.ViewModels.Profile;
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

        Task<int> AddToFavorites(string recipeId, string userId);

        Task SoftDelete(string id);

        Task EditRecipe(RecipeEditViewModel model, string userId);

        Task EditByAdmin(AdminRecipeViewModel model);
    }
}
