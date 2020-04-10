namespace CookingBook.Web.ViewModels.Recipes
{
    using System.ComponentModel.DataAnnotations;

    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;

    public class RecipeByIdProductsViewModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public string RecipeId { get; set; }
    }
}
