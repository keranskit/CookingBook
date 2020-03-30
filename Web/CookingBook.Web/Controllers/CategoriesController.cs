namespace CookingBook.Web.Controllers
{
    using System.Threading.Tasks;
    using Data.Common.Repositories;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Services.Data;

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

    }
}
