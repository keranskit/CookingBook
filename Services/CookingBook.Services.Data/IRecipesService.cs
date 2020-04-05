namespace CookingBook.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Primitives;
    using Web.ViewModels.Recipes;

    public interface IRecipesService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetByCategoryId<T>(int categoryId);

        T GetById<T>(string recipeId);

        Task<string> CreateAsync(RecipeCreateViewModel model, string userId, StringValues sessionKeysList, StringValues sessionValuesList);
    }
}