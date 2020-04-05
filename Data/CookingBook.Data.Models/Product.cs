namespace CookingBook.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Common.Models;

    public class Product : BaseDeletableModel<string>
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Quantity { get; set; }

        [Required]
        public string RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
