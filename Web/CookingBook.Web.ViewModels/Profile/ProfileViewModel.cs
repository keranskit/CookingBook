namespace CookingBook.Web.ViewModels.Profile
{
    using System.Collections.Generic;

    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;

    public class ProfileViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public IEnumerable<RecipeByUserViewModel> Recipes { get; set; }

        public IEnumerable<RecipeByUserFavoriteViewModel> FavoriteRecipes { get; set; }
    }
}
