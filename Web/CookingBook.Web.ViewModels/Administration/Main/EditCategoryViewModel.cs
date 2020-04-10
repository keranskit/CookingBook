namespace CookingBook.Web.ViewModels.Administration.Main
{
    using System.ComponentModel.DataAnnotations;
    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;

    public class EditCategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
