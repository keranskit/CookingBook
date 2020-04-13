namespace CookingBook.Web.ViewModels.Recipes
{
    using System.Collections.Generic;

    public class NewestFiveRecipesViewModel
    {
        public IEnumerable<RecipeByIdViewModel> Recipes { get; set; }
    }
}
