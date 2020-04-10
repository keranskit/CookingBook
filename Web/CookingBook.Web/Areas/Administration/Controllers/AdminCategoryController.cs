namespace CookingBook.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    using CookingBook.Services.Data;
    using CookingBook.Web.ViewModels.Administration.Main;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class AdminCategoryController : AdministrationController
    {
        private readonly ICategoriesService categoriesService;

        public AdminCategoryController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [HttpPost]
        public IActionResult ConfirmEdit(int id, string title)
        {
            var viewModel = new CategoryInMain { Id = id, Title = title };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditById(EditCategoryViewModel viewModel)
        {
            await this.categoriesService.EditById(viewModel.Id, viewModel.Title);
            return this.Content("edited");
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id, string title)
        {
            var viewModel = new CategoryInMain { Id = id, Title = title };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteById(int id)
        {
            await this.categoriesService.DeleteById(id);
            return this.Content("deleted");
        }
    }
}
