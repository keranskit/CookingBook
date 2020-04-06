namespace CookingBook.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CookingBook.Data;
    using CookingBook.Services.Data;
    using CookingBook.Web.ViewModels.Recipes;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Primitives;

    public class RecipesController : Controller
    {
        private readonly IRecipesService recipesService;
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public RecipesController(
            IRecipesService recipesService,
            ICategoriesService categoriesService,
            UserManager<ApplicationUser> userManager)
        {
            this.recipesService = recipesService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
        }

        public IActionResult ById(string id)
        {
            var viewModel = this.recipesService.GetById<RecipeByIdViewModel>(id);

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            var id = Guid.NewGuid().ToString();
            var categories = this.categoriesService.GetAll<CategoryDropdownViewModel>();
            var viewModel = new RecipeCreateViewModel
            {
                Categories = categories,
            };
            return this.View(viewModel);
        }

        // Todo: View beautify
        // Todo: Validate input negative and zero for Product Quantity
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync(RecipeCreateViewModel model)
        {
            var userId = this.userManager.GetUserId(this.User);

            var sessionKeys = this.HttpContext.Request.Form.TryGetValue("Name", out var sessionKeysList);

            var sessionValues = this.HttpContext.Request.Form.TryGetValue("Quantity", out var sessionValuesList);

            if (!(ModelState.IsValid || sessionKeys || sessionValues))
            {
                return this.Content("Something went wrong!");
            }

            if (sessionKeysList.Count != sessionValuesList.Count)
            {
                return this.Content("All products should have values!");
            }

            var recipeId = await this.recipesService.CreateAsync(model, userId, sessionKeysList, sessionValuesList);

            return this.RedirectToAction(nameof(this.ById), new { id = recipeId });
        }

        [Authorize]
        public IActionResult AddReview(string id)
        {
            var model = new ReviewForRecipeViewModel
            {
                RecipeId = id,
            };
            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewForRecipeViewModel model)
        {
            model.UserId = this.userManager.GetUserId(this.User);
            if (!ModelState.IsValid)
            {
                return this.Content("Not this time, Johny!");
            }

            await this.recipesService.AddReview(model);

            return this.RedirectToAction(nameof(this.ById), new { id = model.RecipeId });
        }
    }
}
