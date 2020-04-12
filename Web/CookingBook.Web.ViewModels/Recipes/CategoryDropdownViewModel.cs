namespace CookingBook.Web.ViewModels.Recipes
{
    using System.ComponentModel.DataAnnotations;
    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;

    public class CategoryDropdownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
