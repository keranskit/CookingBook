namespace CookingBook.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using CookingBook.Data.Models;
    using CookingBook.Services.Data;
    using CookingBook.Web.ViewModels.Administration.Main;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AdminUsersController : AdministrationController
    {
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AdminUsersController(
            IUsersService usersService, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> BanUser(string id, DateTime ban)
        {
            await this.usersService.Ban(id, ban);
            return this.Redirect($"/Administration/Main/EditUsers");
        }

        [HttpPost]
        public async Task<IActionResult> UnbanUser(string id)
        {
            await this.usersService.Unban(id);
            return this.Redirect($"/Administration/Main/EditUsers");
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
            var user = this.userManager.GetUserId(this.User);
            await this.usersService.RemoveFromAdmins(id);

            if (this.userManager.GetUserId(this.User) == id)
            {
                await this.signInManager.SignOutAsync();
                return this.Redirect($"/");
            }

            return this.Redirect($"/Administration/Main/EditUsers");
        }
    }
}
