namespace CookingBook.Web.ViewModels.Profile
{
    using AutoMapper;
    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;

    public class RecipeByUserViewModel : IMapFrom<Recipe>
    {
        public string Id { get; set; }

        public string Title { get; set; }
    }
}
