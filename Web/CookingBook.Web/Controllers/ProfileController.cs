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

        /*
        [HttpPost]
        public IActionResult EditMyRecipes()
        {
            var userId = this.userManager.GetUserId(this.User);
            var viewModel = this.profileService.GetByUserId<RecipeByUserViewModel>(userId);
            return this.NotFound();
        }*/

        public IActionResult EditMyFavorites()
        {
            return this.NotFound();
        }

        /*
        [HttpPost]
        public IActionResult EditMyFavorites()
        {
            return this.NotFound();
        } */
    }
}
