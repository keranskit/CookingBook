namespace CookingBook.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserAllergen
    {
        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public int AllergenId { get; set; }

        public virtual Allergen Allergen { get; set; }
    }
}
