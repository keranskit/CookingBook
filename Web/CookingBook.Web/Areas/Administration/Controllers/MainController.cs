namespace CookingBook.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using CookingBook.Data.Common.Repositories;
    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;
    using CookingBook.Web.ViewModels.Administration.Main;
    using Microsoft.AspNetCore.Mvc;

    public class MainController : AdministrationController
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<Recipe> recipeRepository;
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public MainController(
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<Recipe> recipeRepository,
            IDeletableEntityRepository<Category> categoryRepository)
        {
            this.userRepository = userRepository;
            this.recipeRepository = recipeRepository;
            this.categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult EditCategories()
        {
            var categories = this.categoryRepository.All().To<CategoryInMain>().ToList();
            var viewModel = new MainAdminCategoriesViewModel
            {
                Categories = categories,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult EditCategories(int id)
        {
            return this.Content("ok");
        }

        public IActionResult EditRecipes(IEnumerable<Recipe> collection)
        {
            return this.View();
        }

        public IActionResult EditUsers(IEnumerable<ApplicationUser> collection)
        {
            return this.View();
        }
    }
}
