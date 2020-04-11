namespace CookingBook.Web.ViewModels.Administration.Main
{
    using System.Collections.Generic;

    using CookingBook.Data.Models;

    public class MainAdminUsersViewModel
    {
        public IEnumerable<AdminUserViewModel> Users { get; set; }
    }
}
