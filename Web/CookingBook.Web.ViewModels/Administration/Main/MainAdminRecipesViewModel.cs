namespace CookingBook.Web.ViewModels.Administration.Main
{
    using System.Collections.Generic;

    public class MainAdminRecipesViewModel
    {
        public IEnumerable<AdminRecipeViewModel> Recipes { get; set; }
    }
}
