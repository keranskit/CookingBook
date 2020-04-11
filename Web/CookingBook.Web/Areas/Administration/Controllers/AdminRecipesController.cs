namespace CookingBook.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CookingBook.Services.Data;
    using CookingBook.Web.Controllers;
    using CookingBook.Web.ViewModels.Administration.Main;
    using CookingBook.Web.ViewModels.Profile;
    using CookingBook.Web.ViewModels.Recipes;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    public class AdminRecipesController : AdministrationController
    {
        private readonly IRecipesService recipesService;
        private readonly ICategoriesService categoriesService;

        public AdminRecipesController(IRecipesService recipesService, ICategoriesService categoriesService)
        {
            this.recipesService = recipesService;
            this.categoriesService = categoriesService;
        }

        // Todo: beautify the view
        [HttpPost]
        public IActionResult EditRecipe(string id)
        {
            var recipe = this.recipesService.GetById<AdminRecipeViewModel>(id);
            var categories = this.categoriesService.GetAll<CategoryDropdownViewModel>();
            recipe.Categories = categories;
            return this.View(recipe);
        }

        // Todo: beautify the view
        [HttpPost]
        public IActionResult DeleteRecipe(string id)
        {
            var recipe = this.recipesService.GetById<AdminRecipeViewModel>(id);
            return this.View(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteById(string id)
        {
            await this.recipesService.SoftDelete(id);
            return this.Redirect($"/Administration/Main");
        }

        [HttpPost]
        public async Task<IActionResult> EditById(AdminRecipeViewModel recipe)
        {
            await this.recipesService.EditByAdmin(recipe);
            return this.Redirect($"/Administration/Main");
        }
    }
}
