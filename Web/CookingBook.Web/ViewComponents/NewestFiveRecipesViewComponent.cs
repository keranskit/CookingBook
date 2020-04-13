namespace CookingBook.Web.ViewComponents
{
    using System.Threading.Tasks;

    using CookingBook.Services.Data;
    using CookingBook.Web.ViewModels.Recipes;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "NewestFiveRecipes")]
    public class NewestFiveRecipesViewComponent : ViewComponent
    {
        private readonly IRecipesService recipesService;

        public NewestFiveRecipesViewComponent(IRecipesService recipesService)
        {
            this.recipesService = recipesService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = new NewestFiveRecipesViewModel
            {
                Recipes = this.recipesService.GetNewestFiveRecipes<RecipeByIdViewModel>(),
            };
            return this.View(viewModel);
        }
    }
}
