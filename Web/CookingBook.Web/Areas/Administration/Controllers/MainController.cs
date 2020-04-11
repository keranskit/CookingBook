namespace CookingBook.Web.Areas.Administration.Controllers
{
    using System.Linq;

    using CookingBook.Data.Common.Repositories;
    using CookingBook.Data.Models;
    using CookingBook.Services.Data;
    using CookingBook.Services.Mapping;
    using CookingBook.Web.ViewModels.Administration.Main;
    using Microsoft.AspNetCore.Mvc;

    public class MainController : AdministrationController
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<Recipe> recipeRepository;
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly IUsersService usersService;

        public MainController(
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<Recipe> recipeRepository,
            IDeletableEntityRepository<Category> categoryRepository,
            IUsersService usersService)
        {
            this.userRepository = userRepository;
            this.recipeRepository = recipeRepository;
            this.categoryRepository = categoryRepository;
            this.usersService = usersService;
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

        public IActionResult EditRecipes()
        {
            var recipes = this.recipeRepository.All().To<AdminRecipeViewModel>().ToList();
            var viewModel = new MainAdminRecipesViewModel
            {
                Recipes = recipes,
            };
            return this.View(viewModel);
        }

        public IActionResult EditUsers()
        {
            var users = this.usersService.GetAll<AdminUserViewModel>();
            var viewModel = new MainAdminUsersViewModel
            {
                Users = users,
            };
            return this.View(viewModel);
        }
    }
}
