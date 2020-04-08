namespace CookingBook.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using CookingBook.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using CookingBook.Services.Data;
    using CookingBook.Web.ViewModels.Profile;

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
        
        [HttpPost]
        public async Task<IActionResult> DeleteRecipe(string id)
        {
            await this.recipesService.TurnToDeleted(id);
            return this.Content("should be ok");
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
