namespace CookingBook.Web.ViewModels.Recipes
{
    using System.ComponentModel.DataAnnotations;

    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;

    public class RecipeCreateProductsViewModel : IMapFrom<Product>
    {
        [Required]
        public string Product1 { get; set; }

        public string Product2 { get; set; }

        public string Product3 { get; set; }

        public string Product4 { get; set; }

        public string Product5 { get; set; }

        public string Product6 { get; set; }

        public string Product7 { get; set; }

        public string Product8 { get; set; }

        public string Product9 { get; set; }

        public string Product10 { get; set; }

        public string Product11 { get; set; }

        public string Product12 { get; set; }

        public string Product13 { get; set; }

        public string Product14 { get; set; }

        public string Product15 { get; set; }
    }
}
