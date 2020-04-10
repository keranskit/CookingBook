namespace CookingBook.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProfilesService
    {

        IEnumerable<T> GetByUserId<T>(string userId);

        IEnumerable<T> GetFavoriteByUserId<T>(string userId);

        Task RemoveFromFavorites(string userId, string recipeId);
    }
}
