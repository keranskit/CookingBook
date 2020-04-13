namespace CookingBook.Web.Controllers
{
    using CookingBook.Data.Common.Repositories;
    using CookingBook.Data.Models;
    using CookingBook.Services.Data;
    using CookingBook.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IRecipesService recipesService;

        public CategoriesController(
            ICategoriesService categoriesService, 
            IRecipesService recipesService)
        {
            this.categoriesService = categoriesService;
            this.recipesService = recipesService;
        }

        public IActionResult ById(int id)
        {
            var viewModel = this.categoriesService.GetById<CategoryViewModel>(id);
            if (viewModel == null)
            {
                return this.NotFound();
            }
            
            viewModel.Recipes = this.recipesService.GetByCategoryId<RecipeInCategoryViewModel>(id);
            return this.View(viewModel);
        }
    }
}
