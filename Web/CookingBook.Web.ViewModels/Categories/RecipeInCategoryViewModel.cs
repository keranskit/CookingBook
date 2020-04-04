namespace CookingBook.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;

    public class RecipeInCategoryViewModel : IMapFrom<Recipe>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Photo { get; set; }

        public int CookTime { get; set; }

        public int Serving { get; set; }
    }
}
