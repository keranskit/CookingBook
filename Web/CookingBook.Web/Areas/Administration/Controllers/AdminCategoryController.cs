namespace CookingBook.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class AdminCategoryController : AdministrationController
    {
        public AdminCategoryController()
        {
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditById(string id)
        {
            return this.Content("edit by recipe id from ACE");
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteById(string id)
        {
            return this.Content("edit by recipe id from ACD");
        }
    }
}
