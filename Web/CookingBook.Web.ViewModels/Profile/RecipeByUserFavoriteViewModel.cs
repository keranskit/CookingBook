namespace CookingBook.Web.ViewModels.Profile
{
    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;

    public class RecipeByUserFavoriteViewModel : IMapFrom<UserFavoriteRecipe>
    {
        public string RecipeId { get; set; }

        public string RecipeTitle { get; set; }
    }
}
