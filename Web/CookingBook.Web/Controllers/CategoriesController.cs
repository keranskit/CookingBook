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

        public CategoriesController(ICategoriesService categoriesService, IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoriesService = categoriesService;
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categories = this.categoriesService.GetAll<CategoriesViewModel>().ToList();
            var model = new CategoriesListViewModel(){Categories = categories};
            return this.View(model);
        }

    }
}
