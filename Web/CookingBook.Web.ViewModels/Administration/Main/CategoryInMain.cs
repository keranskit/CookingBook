namespace CookingBook.Web.ViewModels.Administration.Main
{
    using System.ComponentModel.DataAnnotations;

    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;

    public class CategoryInMain : IMapFrom<Category>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
    }
}
