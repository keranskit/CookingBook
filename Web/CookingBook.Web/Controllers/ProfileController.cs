namespace CookingBook.Web.Controllers
{
    using System.Net.Mime;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Services.Data;
    using ViewModels.Profile;

    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProfilesService profileService;

        public ProfileController(UserManager<ApplicationUser> userManager, 
            IProfilesService profileService)
        {
            this.userManager = userManager;
            this.profileService = profileService;
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

        public IActionResult EditMyRecipes()
        {
            var userId = this.userManager.GetUserId(this.User);
            var viewModel = this.profileService.GetByUserId<RecipeByUserViewModel>(userId);
            return this.View(viewModel);
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
