namespace CookingBook.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CookingBook.Data.Models;
    using CookingBook.Services.Data;
    using CookingBook.Web.ViewModels.Profile;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Recipes;

    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProfilesService profileService;
        private readonly IRecipesService recipesService;

        public ProfileController(
            UserManager<ApplicationUser> userManager, 
            IProfilesService profileService, 
            IRecipesService recipesService)
        {
            this.userManager = userManager;
            this.profileService = profileService;
            this.recipesService = recipesService;
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
            var recipe = this.recipesService.GetById<Recipe>(id);
            var viewModel = new RecipeCreateViewModel
            {
                CategoryId = recipe.CategoryId,
                Photo = recipe.Photo,
                Title = recipe.Title,
                CookProcedure = recipe.CookProcedure,
                CookTime = recipe.CookTime,
                Serving = recipe.Serving,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdateEditedRecipe(RecipeCreateViewModel model)
        {
            return this.NotFound();
        }
    }
}
