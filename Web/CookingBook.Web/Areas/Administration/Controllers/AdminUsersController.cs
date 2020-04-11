namespace CookingBook.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CookingBook.Data.Models;
    using CookingBook.Services.Data;
    using CookingBook.Web.ViewModels.Administration.Main;
    using Microsoft.AspNetCore.Mvc;

    public class AdminUsersController : AdministrationController
    {
        private readonly IUsersService usersService;

        public AdminUsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult BanUser(string id)
        {
            return this.Ok();
        }

        public IActionResult UnbanUser(string id)
        {
            return this.Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddToAdmins(string id)
        {
            await this.usersService.AddToAdmins(id); 
            return this.Redirect($"/Administration/Main/EditUsers");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmins(string id)
        {
            await this.usersService.RemoveFromAdmins(id);
            return this.Redirect($"/Administration/Main/EditUsers");
        }
    }
}
