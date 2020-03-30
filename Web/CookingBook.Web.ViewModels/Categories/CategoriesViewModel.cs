namespace CookingBook.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;

    public class CategoriesViewModel : IMapFrom<Category>
    {
        public string Title { get; set; }
    }
}
