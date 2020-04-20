namespace CookingBook.Web.ViewModels.Recipes
{
    using System;

    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;

    public class RecipeDTO : IMapFrom<Recipe>
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }
    }
}
