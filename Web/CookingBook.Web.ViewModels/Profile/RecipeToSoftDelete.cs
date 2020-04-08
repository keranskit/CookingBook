namespace CookingBook.Web.ViewModels.Profile
{
    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;

    public class RecipeToSoftDelete : IMapFrom<Recipe>
    {
        public string Id { get; set; }
    }
}
