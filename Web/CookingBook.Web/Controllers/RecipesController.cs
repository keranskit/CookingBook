using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingBook.Web.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data;
    using ViewModels.Recipes;

    public class RecipesController : Controller
    {
        private readonly IRecipesService recipesService;
        private readonly ICategoriesService categoriesService;

        public RecipesController(IRecipesService recipesService, ICategoriesService categoriesService)
        {
            this.recipesService = recipesService;
            this.categoriesService = categoriesService;
        }

        public IActionResult ById(string Id)
        {
            var viewModel = this.recipesService.GetById<RecipeByIdViewModel>(Id);

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            var categories = this.categoriesService.GetAll<CategoryDropdownViewModel>();
            var viewModel = new RecipeCreateViewModel
            {
                Categories = categories,

                // ToDo: create new nutritionValue and Product.
            };
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecipeCreateViewModel model)
        {
            return this.Ok();
        }
    }
}
