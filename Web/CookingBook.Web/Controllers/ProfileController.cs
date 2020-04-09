namespace CookingBook.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CookingBook.Data.Models;
    using CookingBook.Services.Data;
    using CookingBook.Web.ViewModels.Profile;
    using CookingBook.Web.ViewModels.Recipes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProfilesService profileService;
        private readonly IRecipesService recipesService;
        private readonly ICategoriesService categoriesService;

        public ProfileController(
            UserManager<ApplicationUser> userManager, 
            IProfilesService profileService, 
            IRecipesService recipesService, 
            ICategoriesService categoriesService)
        {
            this.userManager = userManager;
            this.profileService = profileService;
            this.recipesService = recipesService;
            this.categoriesService = categoriesService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var userId = this.userManager.GetUserId(this.User);
            var viewModel = new ProfileViewModel
            {
                Id = userId,
                Recipes = this.profileService.GetByUserId<RecipeByUserViewModel>(userId),
                FavoriteRecipes = this.profileService.GetFavoriteByUserId<RecipeByUserFavoriteViewModel>(userId),
            };
            return this.View(viewModel);
        }

        // Todo: edit view view
        [Authorize]
        public IActionResult ConfirmDeleteRecipe(string id)
        {
            var viewModel = new RecipeToSoftDelete { Id = id };
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteRecipe(string id)
        {
            await this.recipesService.SoftDelete(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditRecipe(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var recipe = this.recipesService.GetAll<RecipeEditViewModel>().FirstOrDefault(x => x.Id == id);
            var categories = this.categoriesService.GetAll<CategoryDropdownViewModel>();
            var viewModel = new RecipeEditViewModel()
            {
                Id = recipe.Id,
                Categories = categories,
                Photo = recipe.Photo,
                Title = recipe.Title,
                CookProcedure = recipe.CookProcedure,
                CookTime = recipe.CookTime,
                Serving = recipe.Serving,
                CategoryId = recipe.CategoryId,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEditedRecipe(RecipeEditViewModel model)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.recipesService.EditRecipe(model, userId);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
