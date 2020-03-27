namespace CookingBook.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RecipeAllergen
    {
        [Required]
        public string RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        [Required]
        public int AllergenId { get; set; }

        public virtual Allergen Allergen { get; set; }
    }
}
