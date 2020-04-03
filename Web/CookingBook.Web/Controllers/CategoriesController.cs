namespace CookingBook.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CookingBook.Data.Common.Repositories;
    using CookingBook.Data.Models;
    using CookingBook.Services.Data;
    using CookingBook.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly IRecipesService recipesService;

        public CategoriesController(
            ICategoriesService categoriesService, 
            IDeletableEntityRepository<Category> categoryRepository,
            IRecipesService recipesService)
        {
            this.categoriesService = categoriesService;
            this.categoryRepository = categoryRepository;
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
