namespace CookingBook.Web.ViewModels.Recipes
{
    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;

    public class CategoryDropdownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
